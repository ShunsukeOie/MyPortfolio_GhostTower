using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    [SerializeField, Header("���C�g�I�u�W�F�N�g")]
    private GameObject LightObj;

    // ���C�g���_�����Ă��邩���肷��t���O
    private bool m_isLighting;

    private float FlashCoolTimer;
    private float FlashCoolTime;

    // ���C�g�̃X�N���v�g���i�[����ϐ�
    Light m_lightscript;

    void Start()
    {
        // �X�N���v�g���擾����
        m_lightscript = LightObj.GetComponent<Light>();
        // �t���O��������
        m_isLighting = false;
        // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ��Ă���
        LightObj.SetActive(false);
    }

    void Update()
    {
        // ���C�g��_������
        LightUp();

        if (Input.GetButtonDown("Flash") && m_isLighting)
        { 
           // ���ʂ��グ��
            m_lightscript.intensity = 10f;

            // ���ʂ������Ă����R���[�`���X�^�[�g
            StartCoroutine(Downintensity());
        }
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
            LightObj.SetActive(true);
        }

        // ���C�g���_�����Ă����珈������
        else if (Input.GetButtonDown("Light") && m_isLighting)
        {
            // �_�����Ă��邩�̃t���O��������
            m_isLighting = false;
            // ���C�g�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            LightObj.SetActive(false);
        }
    }

    // ���ʂ�������R���[�`��
    IEnumerator Downintensity()
    {
        // ���[�v��(�l�𑝂₷�Ɗ��炩�ɂȂ�)
        int loopcount = 50;
        // �����肫��܂łɂ����鎞��
        float downtime = 1.0f;        
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
