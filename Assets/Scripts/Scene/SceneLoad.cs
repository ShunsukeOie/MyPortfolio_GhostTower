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
        // 何かボタンを押したら
        if (Input.anyKeyDown)
        {
            // 名前が入ってないならスキップ
            if (m_sSceneName == null) { return; }

            // 指定のシーンをロードする
            SceneManager.LoadScene(m_sSceneName);
        }

    }
}