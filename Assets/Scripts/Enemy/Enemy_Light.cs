using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{
    // Light�̃X�N���v�g�i�[�p
    private Light m_lightScript;

    // ���邩�ǂ����̃t���O
    private bool m_isLighting = false;

    // Enemy.cs��Start�̑O�ɏ�������
    void Awake()
    {
        // �R���|�[�l���g���擾����
        m_lightScript = GetComponent<Light>();
    }

    // �v���C���[��ǂ��Ă��鎞�ɃG�l�~�[�����炷�֐�
    public void UpdateLighting(bool isChase)
    {
        // �G���v���C���[��ǂ��Ă����珈������
        if (isChase)
        {
            // �G�����点��
            m_lightScript.intensity = 50.0f;
        }
        else
        {
            // ��������
            m_lightScript.intensity = 0.0f;
        }
    }

    // �v���C���[���A�C�e�����E�������ɓG�����炷�֐�
    public void FlashLight()
    {
        // �����Ă����Ԃ�������
        if (!m_isLighting)
        {
            m_isLighting=true;

            // ���ʂ��グ��
            m_lightScript.intensity = 50.0f;

            // ���ʂ������Ă����R���[�`���X�^�[�g
            StartCoroutine(DecreaseLightIntensity());
            m_isLighting = false;
        }
    }

    // ���ʂ�������R���[�`��
    IEnumerator DecreaseLightIntensity()
    {
        // ���[�v��(�l�𑝂₷�Ɗ��炩�ɂȂ�)
        int loopcount = 50;
        // �����肫��܂łɂ����鎞��
        float downtime = 0.2f;
        // �E�F�C�g���ԎZ�o
        float waittime = downtime / loopcount;

        for (float intensity = 50f; intensity >= 0.0f; intensity -= 0.25f)
        {
            // �҂�����
            yield return new WaitForSeconds(waittime);

            // ���ʂ����X�ɉ����Ă���
            m_lightScript.intensity = intensity;
        }
    }
}
