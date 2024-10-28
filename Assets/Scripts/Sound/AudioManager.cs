using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // AudioSource格納用
    private AudioSource _audioSource;

    // BGM格納用
    [SerializeField, Header("BGMのオブジェクト")]
    private GameObject[] _bgmobj;

    // プレイヤーの音格納用 (0 = ライト点灯, 1 = ライト消灯, 2 = フラッシュ, 3 = ランタン点灯, 4 = やられ音 5 = アイテムゲット)
    [SerializeField, Header("Player用のSE")]
    private AudioClip[] _playerse;

    // 音を切り替える為のフラグ
    [HideInInspector]
    public bool ChangeAudio = false;
    

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // フラグが下がっていたらデフォルトの音を流す
        if (!ChangeAudio)
        {
            // 敵が追っているとき用のBGMを流さない
            _bgmobj[1].SetActive(false);

            // デフォルトの音を流す
            _bgmobj[0].SetActive(true);
        }
        // フラグが上がっていたら敵が追っているとき用の音を流す
        else
        {
            // デフォルトの音を流さない
            _bgmobj[0].SetActive(false);

            // 敵が追っているとき用の音を流す
            _bgmobj[1].SetActive(true);
        }
    }

    // プレイヤーがライトを点灯する時に流す効果音
    public void LightUpSE()
    {
        // ライト点灯の音を鳴らす
        _audioSource.PlayOneShot(_playerse[0]);
    }

    // プレイヤーがライトを消灯する時に流す効果音
    public void LightDownSE()
    {
        // ライト消灯の音を鳴らす
        _audioSource.PlayOneShot(_playerse[1]);
    }

    // プレイヤーがフラッシュを使用するときに流す効果音
    public void FlashSE()
    {
        // フラッシュの音を鳴らす
        _audioSource.PlayOneShot(_playerse[2]);
    }

    // プレイヤーがランタンを灯す時に流す効果音
    public void LanthanumSE()
    {
        // フラッシュの音を鳴らす
        _audioSource.PlayOneShot(_playerse[3]);
    }

    // プレイヤーがやられた時のに流す効果音
    public void PlayerDeadSE()
    {
        // やられ音を鳴らす
        _audioSource.PlayOneShot(_playerse[4]);
    }

    // プレイヤーがアイテムを入手した時に流す効果音
    public void ItemGetSE()
    {
        // アイテム入手の音を流す
        _audioSource.PlayOneShot(_playerse[5]);
    }
}
