using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    string m_sSceneName = null;

    //��������N���X�̎擾�p�ϐ�
    [SerializeField, Header("��������N���X")]
    private Explanation_controller m_Controller;

    //�}�G�J�E���g�p
    float m_count = 0;

    //�A�Ŗh�~�p�̓��͐����^�C�}�[
    float m_Timer = 1.0f;
    //�{�^�����̓C���^�[�o��
    [SerializeField, Header("�{�^�����̓C���^�[�o��")]
    private float m_Interval;

    // ���i�[�p�i���ʉ��j
    [SerializeField]
    private AudioClip _audio;

    // ����炷���߂ɕK�v�Ȃ��́i�X�s�[�J�[�j
    private AudioSource _audioSource;

    private void Start()
    {
        // �R���|�[�l���g���擾
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���͐����p
        if (m_Timer >= 0.0f)
        {
            m_Timer -= Time.deltaTime;
        }

        // �{�^�����������瑀�������\��
        if (Input.anyKeyDown && m_count == 0)
        {
            // Audio
            _audioSource.clip = _audio;

            StartCoroutine(m_Controller.ChangeColor1());

            // 1
            ++m_count;

            //�^�C�}�[���Z�b�g
            m_Timer = m_Interval;

            // �Đ����Ă��Ȃ��Ƃ�
            if (!_audioSource.isPlaying)
            {
                // ��x�������ʉ���炷
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

        //�V�ѕ���\��
        else if(Input.anyKeyDown && m_count == 1 && m_Timer <= 0.0f && !_audioSource.isPlaying)
        {
            StartCoroutine(m_Controller.ChangeColor2());

            // 2
            ++m_count;

            //�^�C�}�[���Z�b�g
            m_Timer = m_Interval;

            // �Đ����Ă��Ȃ��Ƃ�
            if (!_audioSource.isPlaying)
            {
                // ��x�������ʉ���炷
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

        //�Z���N�g��ʂɑJ��
        else if(Input.anyKeyDown && m_count == 2 && m_Timer <= 0.0f && !_audioSource.isPlaying)
        {
            // 3
            ++m_count;

            // �Đ����Ă��Ȃ��Ƃ�
            if (!_audioSource.isPlaying)
            {
                // ��x�������ʉ���炷
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

        // �Z���N�g��ʂ̎����������Ă��Ȃ��Ƃ�
        if(m_count == 3 && !_audioSource.isPlaying)
        {
            // �w��̃V�[�������[�h����
            SceneManager.LoadScene(m_sSceneName);
        }
    }
}