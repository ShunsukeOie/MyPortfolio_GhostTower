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
        // タグがプレイヤー以外なら終了
        if (collision.gameObject.tag != "Player") { return; }

        // 指定のシーンをロードする
        SceneManager.LoadScene(m_sSceneName);
    }
}
