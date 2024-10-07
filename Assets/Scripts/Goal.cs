using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    string m_sSceneName = null;

    private void OnTriggerEnter(Collider other)
    {
        // タグがプレイヤー以外なら終了
        if (other.gameObject.tag != "Player") { return; }

        // 指定のシーンをロードする
        SceneManager.LoadScene(m_sSceneName);
    }
}
