using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    // ���C�g�I�u�W�F�N�g
    [SerializeField, Header("���C�g�I�u�W�F�N�g")]
    private GameObject m_lightObj;
    // ���C�g�̃X�N���v�g���i�[����ϐ�
    private Light m_lightScript;

    // �t���b�V���𔻒肷��I�u�W�F�N�g
    //�i�ʂɂ��邱�ƂœG�̍��G�͈͂Ƃ̊����Ȃ����j
    [SerializeField, Header("�t���b�V������I�u�W�F�N�g")]
    private GameObject m_judgeObj;
    // �t���b�V������p�̃X�N���v�g�i�[�p
    private Flash_Judge m_JudgeScript;

    //�Q�[�W����N���X�̎擾�p�ϐ�
    [SerializeField, Header("�Q�[�W����N���X")]
    private GaugeController m_gaugeController;

    // �t���b�V���̃C���^�[�o���p�̕ϐ�
    [SerializeField, Header("�t���b�V���̃C���^�[�o��")]
    private float m_useFlashInterval;
    private float m_flashCoolTimer;

    //�o�b�e���[�p
    private float m_maxBattery = 100;     //�ő�d��
    private float m_currentBattery = 100; //���݂̓d��

    [SerializeField, Header("���̓_�ŗp�J�[�u")]
    AnimationCurve m_curve;
    [SerializeField, Header("���̓_�ŃX�s�[�h")]
    private float m_blinkingSpeed;
    // �ŏ��̌��̒l
    private float m_startIntensity;
    // ���̓_�ŗp�^�C�}�[
    float m_blinkingTimer = 0f;

    // ���C�g���_�����Ă��邩���肷��t���O
    private bool m_isLighting;

    void Start()
    {
        // �X�N���v�g���擾����
        m_lightScript = m_lightObj.GetComponent<Light>();
        m_JudgeScript = m_judgeObj.GetComponent<Flash_Judge>();


        // �t���O��������
        m_isLighting = false;
        // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ��Ă���
        m_lightObj.SetActive(false);
        // �ŏ��̌��̒l���擾
        m_startIntensity = m_lightScript.intensity;

        //�Q�[�W�������l�ōX�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);

    }

    void Update()
    {

        //�@�o�b�e���[��0�ɂȂ����烉�C�g������
        if (m_currentBattery <= 0)
        {
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            m_lightObj.SetActive(false);

            // ���C�g�����̉���炷
            AudioManager.instance.LightUpSE();
        }

        // ���C�g��_������
        LightUp();

        // �o�b�e���[�̎c�ʂ�10�ȏ�Ȃ�t���b�V������
        if (m_currentBattery >= 10)
        {
            Flash();
        }

        // ���C�g���_�����Ă����珈������
        if (m_isLighting == true)
        {
            // �o�b�e���[�����Ԍo�߂Ō��炵�Ă���
            BatteryDecrease();
        }


        //�o�b�e���[�̎c�ʂŃQ�[�W�̐F��ύX����
        if (m_currentBattery >= 50)
        {
            //��
            m_gaugeController.ChangeColor1();
        }
        else if (m_currentBattery <= 50 && m_currentBattery >= 20)
        {
            //��
            m_gaugeController.ChangeColor2();
        }
        else
        {
            //��
            m_gaugeController.ChangeColor3();

        }

        if(m_currentBattery <= 50)
        {
            m_blinkingTimer += Time.deltaTime;
            m_lightScript.intensity = m_startIntensity * m_curve.Evaluate(m_blinkingTimer * m_blinkingSpeed);
        }
    }

    // ���C�g��_��������֐�
    void LightUp()
    {

        // ���C�g���_�����Ă��Ȃ������珈������
        if (Input.GetButtonDown("Light") && !m_isLighting && m_currentBattery > 0)
        {
            // �_�����Ă��邩�̃t���O���グ��
            m_isLighting = true;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            m_lightObj.SetActive(true);
            // ���C�g�_���̉���炷
            AudioManager.instance.LightUpSE();
        }

        // ���C�g���_�����Ă����珈������
        else if (Input.GetButtonDown("Light") && m_isLighting)
        {
            // �_�����Ă��邩�̃t���O��������
            m_isLighting = false;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            m_lightObj.SetActive(false);
            // ���C�g�����̉���炷
            AudioManager.instance.LightUpSE();
        }
    }

    // �t���b�V���p�֐�
    void Flash()
    {
        // ���͐����p
        if (m_flashCoolTimer >= 0.0f)
        {
            m_flashCoolTimer -= Time.deltaTime;
        }

        // �{�^���������ꂽ�����C�g���t���Ă��邩�^�C�}�[��0�ȉ����o�b�e���[������ꍇ��������
        if (Input.GetButtonDown("Flash") && m_isLighting && m_flashCoolTimer <= 0.0f && m_currentBattery > 0)
        {
            // �t���b�V������p�I�u�W�F�N�g����G���X�g���擾
            List<Enemy> targets = m_JudgeScript.GetEnemies();

            foreach (var enemy in targets)
            {
                enemy.SetStan(true); // �X�^����t�^
            }

            // ���ʂ��グ��
            m_lightScript.intensity = 100f;

            // ���ʂ������Ă����R���[�`���X�^�[�g
            StartCoroutine(Downintensity());

            // �^�C�}�[�����Z�b�g����
            m_flashCoolTimer = m_useFlashInterval;

            //�o�b�e���[������������
            BatteryFlash();

            // �t���b�V���̉���炷
            AudioManager.instance.FlashSE();
        }
    }

    // ���ʂ�������R���[�`��
    IEnumerator Downintensity()
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
            m_lightScript.intensity = intensity;
        }
    }

    //�d�͂����Ԍo�߂ŏ���
    public void BatteryDecrease()
    {
        // �v������
        float elapsedTime = 0f;
        elapsedTime += Time.deltaTime;

        //�o�b�e���[�����炷�i�O�ɂȂ�����~�߂�j
        if (m_currentBattery >= 0)
        {
            m_currentBattery -= elapsedTime;
        }
        //�d�͂���������̃Q�[�W�̌����ڂ��X�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);

    }

    //�d�͂��t���b�V�����C�g�ŏ���
    public void BatteryFlash()
    {
        if (m_currentBattery >= 10)
        {
            m_currentBattery -= 10;
            //�d�͂���������̃Q�[�W�̌����ڂ��X�V
            m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
        }
    }

    //�A�C�e���œd�͂���
    public void HealBattery()
    {
        m_currentBattery += 50;
        //�d�͂��񕜂�����̃Q�[�W�̌����ڂ��X�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }
}

