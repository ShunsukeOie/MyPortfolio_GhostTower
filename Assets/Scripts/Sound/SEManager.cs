using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    // AudioSource格納用
    private AudioSource _audioSourceSE;

    // SE格納用
    [SerializeField]
    private AudioClip _se;

    // シングルトン
    public static SEManager Instance
    {
        get; private set;
    }

    // スタートよりも優先敵に処理する
    private void Awake()
    {
        // SEManegerが既にある時はDestroyする
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // SEManegerがないときは自オブジェクトを保持
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // コンポーネントを取得
        _audioSourceSE = this.GetComponent<AudioSource>();
    }

    // 音を鳴らす為の関数
    public void SettingPlaySE()
    {
        // 一度だけSEを流す
        _audioSourceSE.PlayOneShot(_se);
    }
}
