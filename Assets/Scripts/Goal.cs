using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // タグがプレイヤー以外なら終了
        if (collision.gameObject.tag != "Player") { return; }

        // どのシーンをクリアしたかを伝える
        SceneController.StageClaer();
    }
}
