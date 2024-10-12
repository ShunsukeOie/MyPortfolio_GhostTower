using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{
    // ���邩�ǂ����̃t���O
    [HideInInspector]
    public bool isLighting = false;
    
    // Light�̃X�N���v�g�i�[�p
    [SerializeField]
    private Light m_lightscript;

    void Start()
    {
        // �R���|�[�l���g���擾����
        m_lightscript = GetComponent<Light>();
    }

    void Update()
    {
        // ����Ԃ�������
       if(isLighting)
       {
            // ���ʂ��グ��
            m_lightscript.intensity = 5f;

            // ���ʂ������Ă����R���[�`���X�^�[�g
            StartCoroutine(Downintensity());
            isLighting = false;
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

        for (float intensity = 5f; intensity >= 0.0f; intensity -= 0.1f)
        {
            // �҂�����
            yield return new WaitForSeconds(waittime);

            // ���ʂ����X�ɉ����Ă���
            m_lightscript.intensity = intensity;
        }
    }
}
