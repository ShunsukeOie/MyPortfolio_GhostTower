using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explanation_controller : MonoBehaviour
{
    //�ύX����I�u�W�F�N�g�i�[�i��������j
    [SerializeField] protected Image _ExplanationImage1;

    //�ύX����I�u�W�F�N�g�i�[�i�V�ѕ��j
    [SerializeField] protected Image _ExplanationImage2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // ��ʂ��t�F�[�h�C��������R�[���`��
    public IEnumerator ChangeColor1()
    {
        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        _ExplanationImage1.color = new Color((255.0f / 255.0f), (255.0f / 255.0f), (255.0f / 255.0f), (0.0f / 255.0f));

        // �t�F�[�h�C���ɂ����鎞�ԁi�b�j���ύX��
        const float fade_time = 0.5f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 10;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            // Alpha�l���������グ��
            Color new_color = _ExplanationImage1.color;
            new_color.a = alpha / 255.0f;
            _ExplanationImage1.color = new_color;

        }
    }
    public IEnumerator ChangeColor2()
    {
        // �t�F�[�h��̐F��ݒ�i���j���ύX��
        _ExplanationImage2.color = new Color((255.0f / 255.0f), (255.0f / 255.0f), (255.0f / 255.0f), (0.0f / 255.0f));

        // �t�F�[�h�C���ɂ����鎞�ԁi�b�j���ύX��
        const float fade_time = 0.5f;

        // ���[�v�񐔁i0�̓G���[�j���ύX��
        const int loop_count = 10;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            // Alpha�l���������グ��
            Color new_color = _ExplanationImage2.color;
            new_color.a = alpha / 255.0f;
            _ExplanationImage2.color = new_color;

        }
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
