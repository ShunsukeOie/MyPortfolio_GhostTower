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

    // 音格納用（効果音）
    [SerializeField]
    private AudioClip _audio;

    // 音を鳴らすために必要なもの（スピーカー）
    private AudioSource _audioSource;

    private void Start()
    {
        // コンポーネントを取得
        _audioSource = GetComponent<AudioSource>();
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
            // Audio
            _audioSource.clip = _audio;

            StartCoroutine(m_Controller.ChangeColor1());

            // 1
            ++m_count;

            //タイマーリセット
            m_Timer = m_Interval;

            // 再生していないとき
            if (!_audioSource.isPlaying)
            {
                // 一度だけ効果音を鳴らす
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

        //遊び方を表示
        else if(Input.anyKeyDown && m_count == 1 && m_Timer <= 0.0f && !_audioSource.isPlaying)
        {
            StartCoroutine(m_Controller.ChangeColor2());

            // 2
            ++m_count;

            //タイマーリセット
            m_Timer = m_Interval;

            // 再生していないとき
            if (!_audioSource.isPlaying)
            {
                // 一度だけ効果音を鳴らす
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

        //セレクト画面に遷移
        else if(Input.anyKeyDown && m_count == 2 && m_Timer <= 0.0f && !_audioSource.isPlaying)
        {
            // 3
            ++m_count;

            // 再生していないとき
            if (!_audioSource.isPlaying)
            {
                // 一度だけ効果音を鳴らす
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

        // セレクト画面の時かつ音が鳴っていないとき
        if(m_count == 3 && !_audioSource.isPlaying)
        {
            // 指定のシーンをロードする
            SceneManager.LoadScene(m_sSceneName);
        }
    }
}