using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{
    // Light�̃X�N���v�g�i�[�p
    private Light _lightscript;

    // Enemy_Search�X�N���v�g�i�[�p
    private Enemy_Search _searchscript;

    // ���邩�ǂ����̃t���O
    [HideInInspector]
    public bool isLighting = false;
    
    void Start()
    {
        // �R���|�[�l���g���擾����
        _lightscript = GetComponent<Light>();
        _searchscript = GetComponent<Enemy_Search>();

    }

    void Update()
    {
        // �G���v���C���[��ǂ��Ă����珈������
        if(_searchscript.isChasePlayer)
        {
            // �G�����点��
            _lightscript.intensity = 50f;
        }
        else
        {
            // ��������
            _lightscript.intensity = 0f;
        }

        // ����Ԃ�������
       if(isLighting)
       {
            // ���ʂ��グ��
            _lightscript.intensity = 50f;

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
        float downtime = 0.2f;
        // �E�F�C�g���ԎZ�o
        float waittime = downtime / loopcount;

        for (float intensity = 50f; intensity >= 0.0f; intensity -= 0.25f)
        {
            // �҂�����
            yield return new WaitForSeconds(waittime);

            // ���ʂ����X�ɉ����Ă���
            _lightscript.intensity = intensity;
        }
    }
}
