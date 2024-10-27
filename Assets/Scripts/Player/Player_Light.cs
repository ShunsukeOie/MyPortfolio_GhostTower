using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    // ���C�g�I�u�W�F�N�g
    [SerializeField, Header("���C�g�I�u�W�F�N�g")]
    private GameObject LightObj;
    // ���C�g�̃X�N���v�g���i�[����ϐ�
    private Light m_lightscript;

    // �t���b�V���𔻒肷��I�u�W�F�N�g
    //�i�ʂɂ��邱�ƂœG�̍��G�͈͂Ƃ̊����Ȃ����j
    [SerializeField, Header("�t���b�V������I�u�W�F�N�g")]
    private GameObject JudgeObje;
    // �t���b�V������p�̃X�N���v�g�i�[�p
    private Flash_Judge _Judge;

    //�Q�[�W����N���X�̎擾�p�ϐ�
    [SerializeField, Header("�Q�[�W����N���X")]
    private GaugeController m_gaugeController;

    // �t���b�V���̃C���^�[�o���p�̕ϐ�
    [SerializeField, Header("�t���b�V���̃C���^�[�o��")]
    private float UseFlashInterval;
    private float FlashCoolTimer;

    //�o�b�e���[�p
    private float m_maxBattery = 100;     //�ő�d��
    private float m_currentBattery = 100; //���݂̓d��

    [SerializeField, Header("���̓_�ŗp�J�[�u")]
    AnimationCurve _curve;
    [SerializeField, Header("���̓_�ŃX�s�[�h")]
    private float blinkingspeed;
    // �ŏ��̌��̒l
    private float startintensity;
    // ���̓_�ŗp�^�C�}�[
    float blinkingTimer = 0f;

    // ���C�g���_�����Ă��邩���肷��t���O
    private bool isLighting;


    [SerializeField, Header("AudioManger�I�u�W�F�N�g")]
    private GameObject _audiomng;
    // AudioManager�̃X�N���v�g�i�[�p
    private AudioManager _audioscript;

    //--------------------------------------------
    // public
    //--------------------------------------------

    // �G�l�~�[���X�^���ł����Ԃ��𔻒肷��t���O
    [HideInInspector]
    public bool canStopEnemy = false;

    void Start()
    {
        // �X�N���v�g���擾����
        m_lightscript = LightObj.GetComponent<Light>();
        _Judge = JudgeObje.GetComponent<Flash_Judge>();
        _audioscript = _audiomng.GetComponent<AudioManager>();

        // �t���O��������
        isLighting = false;
        // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ��Ă���
        LightObj.SetActive(false);
        // �ŏ��̌��̒l���擾
        startintensity = m_lightscript.intensity;

        //�Q�[�W�������l�ōX�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);

    }

    void Update()
    {

        //�@�o�b�e���[��0�ɂȂ����烉�C�g������
        if (m_currentBattery <= 0)
        {
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            LightObj.SetActive(false);

            // ���C�g�����̉���炷
            _audioscript.LightUpSE();
        }

        // ���C�g��_������
        LightUp();

        // �o�b�e���[�̎c�ʂ�10�ȏ�Ȃ�t���b�V������
        if (m_currentBattery >= 10)
        {
            Flash();
        }

        // ���C�g���_�����Ă����珈������
        if (isLighting == true)
        {
            // �o�b�e���[�����Ԍo�߂Ō��炵�Ă���
            BatteryDecrease();
        }


        //�o�b�e���[�̎c�ʂŃQ�[�W�̐F��ύX����
        if (m_currentBattery >= 50)
        {
            //��
            m_gaugeController.ChangeColor1();
            //Debug.Log("1");
        }
        else if (m_currentBattery <= 50 && m_currentBattery >= 20)
        {
            //��
            m_gaugeController.ChangeColor2();
            //Debug.Log("2");
        }
        else
        {
            //��
            m_gaugeController.ChangeColor3();

        }

        if(m_currentBattery <= 50)
        {
            blinkingTimer += Time.deltaTime;
            m_lightscript.intensity = startintensity * _curve.Evaluate(blinkingTimer * blinkingspeed);
        }
    }

    // ���C�g��_��������֐�
    void LightUp()
    {

        // ���C�g���_�����Ă��Ȃ������珈������
        if (Input.GetButtonDown("Light") && !isLighting && m_currentBattery > 0)
        {
            // �_�����Ă��邩�̃t���O���グ��
            isLighting = true;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            LightObj.SetActive(true);
            // ���C�g�_���̉���炷
            _audioscript.LightUpSE();

        }

        // ���C�g���_�����Ă����珈������
        else if (Input.GetButtonDown("Light") && isLighting)
        {
            // �_�����Ă��邩�̃t���O��������
            isLighting = false;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            LightObj.SetActive(false);
            // ���C�g�����̉���炷
            _audioscript.LightUpSE();
        }
    }

    // �t���b�V���p�֐�
    void Flash()
    {
        // ���͐����p
        if (FlashCoolTimer >= 0.0f)
        {
            FlashCoolTimer -= Time.deltaTime;
        }

        // �{�^���������ꂽ�����C�g���t���Ă��邩�^�C�}�[��0�ȉ����o�b�e���[������ꍇ��������
        if (Input.GetButtonDown("Flash") && isLighting && FlashCoolTimer <= 0.0f && m_currentBattery > 0)
        {
            // �G�̓������~�߂邱�Ƃ��\�ȏ�ԂȂ珈������
            if (canStopEnemy)
            {
                // �t���b�V���̔���p�X�N���v�g�ɃA�N�Z�X���Anull���ǂ������肷��
                if (_Judge._esearch != null)
                {
                    // �G�X�N���v�g�ɃA�N�Z�X���X�^����Ԃɂ���
                    _Judge._esearch.isStan = true;
                }
            }
            // ���ʂ��グ��
            m_lightscript.intensity = 100f;

            // ���ʂ������Ă����R���[�`���X�^�[�g
            StartCoroutine(Downintensity());

            // �^�C�}�[�����Z�b�g����
            FlashCoolTimer = UseFlashInterval;

            //�o�b�e���[������������
            BatteryFlash();

            // �t���b�V���̉���炷
            _audioscript.FlashSE();
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
            m_lightscript.intensity = intensity;
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

