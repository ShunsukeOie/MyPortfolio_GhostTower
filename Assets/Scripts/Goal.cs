using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �^�O���v���C���[�ȊO�Ȃ�I��
        if (collision.gameObject.tag != "Player") { return; }

        // �ǂ̃V�[�����N���A��������`����
        SceneController.StageClaer();
    }
}
