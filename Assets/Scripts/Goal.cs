using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    string m_sSceneName = null;


    private void OnCollisionEnter(Collision collision)
    {
        // �^�O���v���C���[�ȊO�Ȃ�I��
        if (collision.gameObject.tag != "Player") { return; }

        // �w��̃V�[�������[�h����
        SceneManager.LoadScene(m_sSceneName);
    }
}
