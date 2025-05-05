using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // タグがプレイヤーなら処理する
        if (other.gameObject.tag == "Player") 
        {
            SceneController.CurrentSceneName();

            // どのシーンをクリアしたかを伝える
            SceneController.StageClaer();
        }
    }
}
