using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    string m_sSceneName = null;

    // Update is called once per frame
    void Update()
    {
        // �����{�^������������
        if (Input.anyKeyDown)
        {
            // ���O�������ĂȂ��Ȃ�X�L�b�v
            if (m_sSceneName == null) { return; }

            // �w��̃V�[�������[�h����
            SceneManager.LoadScene(m_sSceneName);
        }

    }
}