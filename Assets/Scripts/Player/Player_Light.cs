using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    // ���C�g�I�u�W�F�N�g
    [SerializeField, Header("���C�g�I�u�W�F�N�g")]
    private GameObject m_LightObj;

    // ���C�g���_�����Ă��邩���肷��t���O
    private bool m_isLighting;

    // �t���b�V���̃C���^�[�o���p�̕ϐ�
    [SerializeField]
    private float m_UseFlashInterval;
    private float m_FlashCoolTimer;
    
    // ���C�g�̃X�N���v�g���i�[����ϐ�
    Light m_lightscript;

    //�o�b�e���[�p
    private float m_maxBattery = 100;     //�ő�d��
    private float m_currentBattery = 100; //���݂̓d��

    //�Q�[�W����N���X�̎擾
    [SerializeField] private GaugeController m_gaugeController;

 
    void Start()
    {
        // �X�N���v�g���擾����
        m_lightscript = m_LightObj.GetComponent<Light>();
        // �t���O��������
        m_isLighting = false;
        // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ��Ă���
        m_LightObj.SetActive(false);

        //�Q�[�W�������l�ōX�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);

    }

    void Update()
    {

        //�@�o�b�e���[��0�ɂȂ����烉�C�g������
        if(m_currentBattery <= 0)
        {
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            m_LightObj.SetActive(false);
        }

        // ���C�g��_������
        LightUp();
        
        // �o�b�e���[�̎c�ʂ�10�ȏ�Ȃ�t���b�V������
        if(m_currentBattery >= 10)
        {
            Flash();
        }
        

        if(m_isLighting == true)
        {
            BatteryDecrease();
        }


        //�o�b�e���[�̎c�ʂŃQ�[�W�̐F��ύX����
        if(m_currentBattery >= 50)
        {
            //��
            m_gaugeController.ChangeColor1();
            //Debug.Log("1");
        }
        else if(m_currentBattery <= 50 && m_currentBattery >= 20)
        {
            //��
            m_gaugeController.ChangeColor2();
            //Debug.Log("2");
        }
        else
        {
            //��
            m_gaugeController.ChangeColor3();
            Debug.Log("3");
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
            m_LightObj.SetActive(true);

        }

        // ���C�g���_�����Ă����珈������
        else if (Input.GetButtonDown("Light") && m_isLighting)
        {
            // �_�����Ă��邩�̃t���O��������
            m_isLighting = false;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            m_LightObj.SetActive(false);

        }
    }

    // �t���b�V���p�֐�
    void Flash()
    {
        // ���͐����p
        if (m_FlashCoolTimer >= 0.0f)
        {
            m_FlashCoolTimer -= Time.deltaTime;
        }

        // �{�^���������ꂽ�����C�g���t���Ă��邩�^�C�}�[��0�ȉ��̏ꍇ��������
        if (Input.GetButtonDown("Flash") && m_isLighting && m_FlashCoolTimer <= 0.0f && m_currentBattery > 0)
        {
            // ���ʂ��グ��
            m_lightscript.intensity = 10f;

            // ���ʂ������Ă����R���[�`���X�^�[�g
            StartCoroutine(Downintensity());

            // �^�C�}�[�����Z�b�g����
            m_FlashCoolTimer = m_UseFlashInterval;

            //�o�b�e���[������������
            BatteryFlash();
        }
    }

    // ���ʂ�������R���[�`��
    IEnumerator Downintensity()
    {
        // ���[�v��(�l�𑝂₷�Ɗ��炩�ɂȂ�)
        int loopcount = 50;
        // �����肫��܂łɂ����鎞��
        float downtime = 0.7f;        
        // �E�F�C�g���ԎZ�o
        float waittime = downtime / loopcount;

        for (float intensity = 10f; intensity >= 2.73f; intensity -= 0.1f)
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
        if(m_currentBattery >= 10)
        {
            m_currentBattery -= 10;
            //�d�͂���������̃Q�[�W�̌����ڂ��X�V
            m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
        }
        
    }

    //�A�C�e���œd�͂���
    public void HealBattery()
    {
        m_currentBattery += 10;
        //�d�͂��񕜂�����̃Q�[�W�̌����ڂ��X�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }
}
