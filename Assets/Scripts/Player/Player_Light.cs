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

    void Start()
    {
        // �X�N���v�g���擾����
        m_lightscript = m_LightObj.GetComponent<Light>();
        // �t���O��������
        m_isLighting = false;
        // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ��Ă���
        m_LightObj.SetActive(false);
    }

    void Update()
    {
        // ���C�g��_������
        LightUp();

        // �t���b�V������
        Flash();
    }

    // ���C�g��_��������֐�
    void LightUp()
    {
        // ���C�g���_�����Ă��Ȃ������珈������
        if (Input.GetButtonDown("Light") && !m_isLighting)
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
        if (Input.GetButtonDown("Flash") && m_isLighting && m_FlashCoolTimer <= 0.0f)
        {
            // ���ʂ��グ��
            m_lightscript.intensity = 10f;

            // ���ʂ������Ă����R���[�`���X�^�[�g
            StartCoroutine(Downintensity());

            // �^�C�}�[�����Z�b�g����
            m_FlashCoolTimer = m_UseFlashInterval;
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
}
