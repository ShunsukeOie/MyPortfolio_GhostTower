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

    private void Start()
    {
        
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
            StartCoroutine(m_Controller.ChangeColor1());
            ++m_count;

            //�^�C�}�[���Z�b�g
            m_Timer = m_Interval;
        }
        //�V�ѕ���\��
        else if(Input.anyKeyDown && m_count == 1 && m_Timer <= 0.0f)
        {
            StartCoroutine(m_Controller.ChangeColor2());
            ++m_count;

            //�^�C�}�[���Z�b�g
            m_Timer = m_Interval;
        }
        //�Z���N�g��ʂɑJ��
        else if(Input.anyKeyDown && m_Timer <= 0.0f)
        {
            // ���O�������ĂȂ��Ȃ�X�L�b�v
            if (m_sSceneName == null) { return; }

            // �w��̃V�[�������[�h����
            SceneManager.LoadScene(m_sSceneName);
        }


    }
}