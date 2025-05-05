using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    [SerializeField, Header("���C�g�I�u�W�F�N�g")]
    private GameObject m_lightObj;

    [SerializeField, Header("�Q�[�W����N���X")]
    private GaugeController m_gaugeController;

    [SerializeField, Header("���̓_�ŗp�J�[�u")]
    private AnimationCurve m_curve;

    [SerializeField, Header("�t���b�V���̃C���^�[�o��")]
    private float m_useFlashInterval;

    [SerializeField, Header("���̓_�ŃX�s�[�h")]
    private float m_blinkingSpeed;

    // ���C�g�̃X�N���v�g���i�[����ϐ�
    private Light m_lightComp;

    // �t���b�V������p�̃X�N���v�g�i�[�p
    private Player_Vision m_flashJudgeComp;

    // �R���[�`�������d�N���N�����Ȃ��悤�ɂ��邽�߂̕ϐ�
    private Coroutine m_coroutine;

    // ���݂̓d��
    private float m_currentBattery = 100f;

    // �ŏ��̌��̒l
    private float m_startIntensity;

    // �t���b�V���̃N�[���^�C�}�[
    private float m_flashCoolTimer;

    // ���̓_�ŗp�^�C�}�[
    private float m_blinkingTimer = 0f;

    // ���C�g���_�����Ă��邩���肷��t���O
    private bool m_isLighting = false;

    // �o�b�e���[�����邩�𔻒肷��t���O
    private bool m_isBatteryDepleted = false;

    // �萔
    private const float MAX_BATTERY = 100f;         //�ő�d��
    private const float FLASH_BATTERY_COST = 10f;   // �t���b�V���Ŏg���o�b�e���[�̃R�X�g
    private const float HEAL_BATTERY_VALUE = 50f;   // �񕜂���o�b�e���[�̗�

    void Awake()
    {
        // �X�N���v�g���擾����
        m_flashJudgeComp = GetComponent<Player_Vision>();
        m_lightComp = m_lightObj.GetComponent<Light>();

        // �ŏ��̌��̒l���擾
        m_startIntensity = m_lightComp.intensity;

        // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ��Ă���
        m_lightObj.SetActive(false);

        //�Q�[�W�������l�ōX�V
        m_gaugeController.UpdateGauge(m_currentBattery, MAX_BATTERY);
    }

    void Update()
    {
        // �o�b�e���[�̎c�ʂ��X�V
        UpdateBattery();

        // ���C�g��_������
        LightUp();

        // �o�b�e���[�̎c�ʂ�10�ȏ�Ȃ�t���b�V���o����
        if (m_currentBattery >= FLASH_BATTERY_COST)
        {
            Flash();
        }

        // �o�b�e���[�̎c�ʂŃQ�[�W�̐F��ύX����
        UpdateGaugeColor();
    }

    // ���C�g��_��������֐�
    void LightUp()
    {
        if (m_currentBattery <= 0f) return;

        // ���C�g���_�����Ă��Ȃ������珈������
        if (Input.GetButtonDown("Light") && !m_isLighting)
        {
            // �_�����Ă��邩�̃t���O���グ��
            m_isLighting = true;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            m_lightObj.SetActive(true);
            // ���C�g�_���̉���炷
            AudioManager.Instance.LightUpSE();
        }

        // ���C�g���_�����Ă����珈������
        else if (Input.GetButtonDown("Light") && m_isLighting)
        {
            // �_�����Ă��邩�̃t���O��������
            m_isLighting = false;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            m_lightObj.SetActive(false);
            // ���C�g�����̉���炷
            AudioManager.Instance.LightUpSE();
        }
    }

    // �t���b�V���p�֐�
    void Flash()
    {
        // ���͐����p
        if (m_flashCoolTimer >= 0f)
        {
            m_flashCoolTimer -= Time.deltaTime;
        }

        // �{�^���������ꂽ�����C�g���t���Ă��邩�^�C�}�[��0�ȉ����o�b�e���[������ꍇ��������
        if (Input.GetButtonDown("Flash") && m_isLighting && m_flashCoolTimer <= 0.0f && m_currentBattery > 0)
        {
            // �t���b�V������p�I�u�W�F�N�g����G���X�g���擾
            List<Enemy> targets = m_flashJudgeComp.GetEnemies();

            foreach (var enemy in targets)
            {
                // �X�^����t�^
                enemy.SetStan(true);
            }

            // ���ʂ��グ��
            m_lightComp.intensity = 100f;

            // ����������R���[�`�����Ă΂�邱�Ƃ���������Â��R���[�`�����~�߂�
            if(m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
            }

            // ���ʂ������Ă����R���[�`���X�^�[�g
            m_coroutine = StartCoroutine(DecreaseLightIntensity());

            // �^�C�}�[�����Z�b�g����
            m_flashCoolTimer = m_useFlashInterval;

            //�o�b�e���[������������
            BatteryFlash();

            // �t���b�V���̉���炷
            AudioManager.Instance.FlashSE();
        }
    }

    // �o�b�e���[�̎c�ʂŃQ�[�W�̐F��ύX����֐�
    void UpdateGaugeColor()
    {
        if (m_currentBattery >= 50f)
        {
            //��
            m_gaugeController.ChangeColor1();
        }
        else if (m_currentBattery <= 50.0f && m_currentBattery >= 20f)
        {
            //��
            m_gaugeController.ChangeColor2();
        }
        else
        {
            //��
            m_gaugeController.ChangeColor3();
        }
    }

    // ���ʂ�������R���[�`��
    IEnumerator DecreaseLightIntensity()
    {
        // ���[�v��(�l�𑝂₷�Ɗ��炩�ɂȂ�)
        int loopcount = 50;
        // ������̂ɂ����鎞��
        float downtime = 0.1f;
        // �E�F�C�g���ԎZ�o
        float waittime = downtime / loopcount;

        for (float intensity = 100f; intensity >= 10f; intensity -= 1f)
        {
            // �҂�����
            yield return new WaitForSeconds(waittime);

            // ���ʂ����X�ɉ����Ă���
            m_lightComp.intensity = intensity;
        }
        // ��Ԃ��N���A
        m_coroutine = null;
    }

    void UpdateBattery()
    {
        if (m_currentBattery > 0 && m_isLighting)
        {
            float newBattery = m_currentBattery -= Time.deltaTime;
            //�o�b�e���[�����炷
            SetBattery(newBattery);

            // �o�b�e���[��50�ȉ��ɂȂ�����
            if (m_currentBattery <= 50f)
            {
                // �J�[�u���g�p���ă��C�g���`�J�`�J����\��������
                m_blinkingTimer += Time.deltaTime;
                m_lightComp.intensity = m_startIntensity * m_curve.Evaluate(m_blinkingTimer * m_blinkingSpeed);
            }
        }

        //�@�o�b�e���[��0�ɂȂ����烉�C�g������
        if (m_currentBattery <= 0 && !m_isBatteryDepleted)
        {
            m_isBatteryDepleted = true;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            m_lightObj.SetActive(false);
            // ���C�g�����̉���炷
            AudioManager.Instance.LightUpSE();
        }
    }

    //�d�͂��t���b�V�����C�g�ŏ���
    public void BatteryFlash()
    {
        float newBattery = m_currentBattery -= FLASH_BATTERY_COST;
        // �o�b�e���[��0�`MAX�̒l�ŕ␳���A�Q�[�W���X�V����
        SetBattery(newBattery);
    }

    //�A�C�e���œd�͂���
    public void HealBattery()
    {
        float newBattery = m_currentBattery += HEAL_BATTERY_VALUE;
        // �o�b�e���[��0�`MAX�̒l�ŕ␳���A�Q�[�W���X�V����
        SetBattery(newBattery);

        // 0�ȉ��̎��Ƀo�b�e���[���񕜂��ꂽ��
        if(m_currentBattery < 0)
        {
            // �o�b�e���[�����邩���肷��t���O���グ��
            m_isBatteryDepleted = false;
        }
    }

    // �o�b�e���[��0�`MAX�̒l�ŕ␳���A�Q�[�W���X�V����֐�
    void SetBattery(float batteryValue)
    {
        m_currentBattery = Mathf.Clamp(batteryValue, 0f, MAX_BATTERY);
        m_gaugeController.UpdateGauge(m_currentBattery, MAX_BATTERY);
    }
}

