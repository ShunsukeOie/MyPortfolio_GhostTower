using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    string m_sSceneName = null;

    //操作説明クラスの取得用変数
    [SerializeField, Header("操作説明クラス")]
    private Explanation_controller m_Controller;

    //挿絵カウント用
    float m_count = 0;

    //連打防止用の入力制限タイマー
    float m_Timer = 1.0f;
    //ボタン入力インターバル
    [SerializeField, Header("ボタン入力インターバル")]
    private float m_Interval;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 入力制限用
        if (m_Timer >= 0.0f)
        {
            m_Timer -= Time.deltaTime;
        }

        // ボタンを押したら操作説明を表示
        if (Input.anyKeyDown && m_count == 0)
        {
            StartCoroutine(m_Controller.ChangeColor1());
            ++m_count;

            //タイマーリセット
            m_Timer = m_Interval;
        }
        //遊び方を表示
        else if(Input.anyKeyDown && m_count == 1 && m_Timer <= 0.0f)
        {
            StartCoroutine(m_Controller.ChangeColor2());
            ++m_count;

            //タイマーリセット
            m_Timer = m_Interval;
        }
        //セレクト画面に遷移
        else if(Input.anyKeyDown && m_Timer <= 0.0f)
        {
            // 名前が入ってないならスキップ
            if (m_sSceneName == null) { return; }

            // 指定のシーンをロードする
            SceneManager.LoadScene(m_sSceneName);
        }


    }
}