using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �^�O���v���C���[�Ȃ珈������
        if (other.gameObject.tag == "Player") 
        {
            SceneController.CurrentSceneName();

            // �ǂ̃V�[�����N���A��������`����
            SceneController.StageClaer();
        }
    }
}
