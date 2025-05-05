using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    // AudioSource格納用
    private AudioSource _audioSource;

    // BGM格納用
    [SerializeField, Header("BGMのオブジェクト")]
    private GameObject[] _bgmObj;

    // プレイヤーの音格納用 (0 = ライト点灯, 1 = ライト消灯, 2 = フラッシュ, 3 = ランタン点灯, 4 = やられ音 5 = アイテムゲット)
    [SerializeField, Header("Player用のSE")]
    private AudioClip[] _playerSE;

    // 何体の敵がプレイヤーを視認しているかを管理するカウンター
    private int _visibleCount = 0;

    // 音を切り替える為のフラグ
    private bool _changeAudio = false;

    private void Awake()
    {
        // インスタンスがなければ自分を設定
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            // すでにあるならば自分を削除する
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // コンポーネントを取得
        _audioSource = GetComponent<AudioSource>();
        // 通常BGMを流す
        ApplyBGMState(false);
    }

    private void ApplyBGMState(bool isChased)
    {
        if (_bgmObj.Length < 2) return;

        // 通常BGM
        _bgmObj[0].SetActive(!isChased);
        // 敵に追われているときのBGM
        _bgmObj[1].SetActive(isChased);
    }

    // 敵がプレイヤーを視認した時呼ばれる関数
    public void RegisterEnemyVision()
    {
        _visibleCount++;
        if(!_changeAudio)
        {
            _changeAudio = true;
            ApplyBGMState(true);
        }        
    }

    // 敵が視界から外れた時呼ばれる関数
    public void UnregisterEnemyVision()
    {
        _visibleCount = Mathf.Max(_visibleCount - 1, 0);
        if(_visibleCount == 0 && _changeAudio)
        {
            _changeAudio = false;
            ApplyBGMState(false);
        }
    }

    // プレイヤーがライトを点灯する時に流す効果音
    public void LightUpSE()
    {
        // ライト点灯の音を鳴らす
        _audioSource.PlayOneShot(_playerSE[0]);
    }

    // プレイヤーがライトを消灯する時に流す効果音
    public void LightDownSE()
    {
        // ライト消灯の音を鳴らす
        _audioSource.PlayOneShot(_playerSE[1]);
    }

    // プレイヤーがフラッシュを使用するときに流す効果音
    public void FlashSE()
    {
        // フラッシュの音を鳴らす
        _audioSource.PlayOneShot(_playerSE[2]);
    }

    // プレイヤーがランタンを灯す時に流す効果音
    public void LanthanumSE()
    {
        // フラッシュの音を鳴らす
        _audioSource.PlayOneShot(_playerSE[3]);
    }

    // プレイヤーがやられた時のに流す効果音
    public void PlayerDeadSE()
    {
        // やられ音を鳴らす
        _audioSource.PlayOneShot(_playerSE[4]);
    }

    // プレイヤーがアイテムを入手した時に流す効果音
    public void ItemGetSE()
    {
        // アイテム入手の音を流す
        _audioSource.PlayOneShot(_playerSE[5]);
    }
}
