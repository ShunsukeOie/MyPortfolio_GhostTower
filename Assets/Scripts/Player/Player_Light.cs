using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    // ライトオブジェクト
    [SerializeField, Header("ライトオブジェクト")]
    private GameObject LightObj;
    // ライトのスクリプトを格納する変数
    private Light m_lightscript;

    // フラッシュを判定するオブジェクト
    //（別にすることで敵の索敵範囲との干渉をなくす）
    [SerializeField, Header("フラッシュ判定オブジェクト")]
    private GameObject JudgeObje;
    // フラッシュ判定用のスクリプト格納用
    private Flash_Judge _Judge;

    //ゲージ操作クラスの取得用変数
    [SerializeField, Header("ゲージ操作クラス")]
    private GaugeController m_gaugeController;

    // フラッシュのインターバル用の変数
    [SerializeField, Header("フラッシュのインターバル")]
    private float UseFlashInterval;
    private float FlashCoolTimer;

    //バッテリー用
    private float m_maxBattery = 100;     //最大電力
    private float m_currentBattery = 100; //現在の電力

    [SerializeField, Header("光の点滅用カーブ")]
    AnimationCurve _curve;
    [SerializeField, Header("光の点滅スピード")]
    private float blinkingspeed;
    // 最初の光の値
    private float startintensity;
    // 光の点滅用タイマー
    float blinkingTimer = 0f;

    // ライトが点灯しているか判定するフラグ
    private bool isLighting;


    [SerializeField, Header("AudioMangerオブジェクト")]
    private GameObject _audiomng;
    // AudioManagerのスクリプト格納用
    private AudioManager _audioscript;

    //--------------------------------------------
    // public
    //--------------------------------------------

    // エネミーがスタンできる状態かを判定するフラグ
    [HideInInspector]
    public bool canStopEnemy = false;

    void Start()
    {
        // スクリプトを取得する
        m_lightscript = LightObj.GetComponent<Light>();
        _Judge = JudgeObje.GetComponent<Flash_Judge>();
        _audioscript = _audiomng.GetComponent<AudioManager>();

        // フラグを初期化
        isLighting = false;
        // ライトオブジェクトを非アクティブにしておく
        LightObj.SetActive(false);
        // 最初の光の値を取得
        startintensity = m_lightscript.intensity;

        //ゲージを初期値で更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);

    }

    void Update()
    {

        //　バッテリーが0になったらライトを消す
        if (m_currentBattery <= 0)
        {
            // ライトオブジェクトを非アクティブにする
            LightObj.SetActive(false);

            // ライト消灯の音を鳴らす
            _audioscript.LightUpSE();
        }

        // ライトを点灯する
        LightUp();

        // バッテリーの残量が10以上ならフラッシュする
        if (m_currentBattery >= 10)
        {
            Flash();
        }

        // ライトが点灯していたら処理する
        if (isLighting == true)
        {
            // バッテリーを時間経過で減らしていく
            BatteryDecrease();
        }


        //バッテリーの残量でゲージの色を変更する
        if (m_currentBattery >= 50)
        {
            //緑
            m_gaugeController.ChangeColor1();
            //Debug.Log("1");
        }
        else if (m_currentBattery <= 50 && m_currentBattery >= 20)
        {
            //黄
            m_gaugeController.ChangeColor2();
            //Debug.Log("2");
        }
        else
        {
            //赤
            m_gaugeController.ChangeColor3();

        }

        if(m_currentBattery <= 50)
        {
            blinkingTimer += Time.deltaTime;
            m_lightscript.intensity = startintensity * _curve.Evaluate(blinkingTimer * blinkingspeed);
        }
    }

    // ライトを点灯させる関数
    void LightUp()
    {

        // ライトが点灯していなかったら処理する
        if (Input.GetButtonDown("Light") && !isLighting && m_currentBattery > 0)
        {
            // 点灯しているかのフラグを上げる
            isLighting = true;
            // ライトオブジェクトをアクティブにする
            LightObj.SetActive(true);
            // ライト点灯の音を鳴らす
            _audioscript.LightUpSE();

        }

        // ライトが点灯していたら処理する
        else if (Input.GetButtonDown("Light") && isLighting)
        {
            // 点灯しているかのフラグを下げる
            isLighting = false;
            // ライトオブジェクトを非アクティブにする
            LightObj.SetActive(false);
            // ライト消灯の音を鳴らす
            _audioscript.LightUpSE();
        }
    }

    // フラッシュ用関数
    void Flash()
    {
        // 入力制限用
        if (FlashCoolTimer >= 0.0f)
        {
            FlashCoolTimer -= Time.deltaTime;
        }

        // ボタンが押されたかつライトが付いているかつタイマーが0以下かつバッテリーがある場合処理する
        if (Input.GetButtonDown("Flash") && isLighting && FlashCoolTimer <= 0.0f && m_currentBattery > 0)
        {
            // 敵の動きを止めることが可能な状態なら処理する
            if (canStopEnemy)
            {
                // フラッシュの判定用スクリプトにアクセスし、nullかどうか判定する
                if (_Judge._esearch != null)
                {
                    // 敵スクリプトにアクセスしスタン状態にする
                    _Judge._esearch.isStan = true;
                }
            }
            // 光量を上げる
            m_lightscript.intensity = 100f;

            // 光量を下げていくコルーチンスタート
            StartCoroutine(Downintensity());

            // タイマーをリセットする
            FlashCoolTimer = UseFlashInterval;

            //バッテリーを減少させる
            BatteryFlash();

            // フラッシュの音を鳴らす
            _audioscript.FlashSE();
        }
    }

    // 光量を下げるコルーチン
    IEnumerator Downintensity()
    {
        // ループ回数(値を増やすと滑らかになる)
        int loopcount = 50;
        // 下がるのにかかる時間
        float downtime = 0.1f;
        // ウェイト時間算出
        float waittime = downtime / loopcount;

        for (float intensity = 100f; intensity >= 10f; intensity -= 1f)
        {
            // 待ち時間
            yield return new WaitForSeconds(waittime);

            // 光量を徐々に下げていく
            m_lightscript.intensity = intensity;
        }
    }

    //電力を時間経過で消費
    public void BatteryDecrease()
    {
        // 計測時間
        float elapsedTime = 0f;
        elapsedTime += Time.deltaTime;

        //バッテリーを減らす（０になったら止める）
        if (m_currentBattery >= 0)
        {
            m_currentBattery -= elapsedTime;
        }
        //電力が減った後のゲージの見た目を更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);

    }


    //電力をフラッシュライトで消費
    public void BatteryFlash()
    {
        if (m_currentBattery >= 10)
        {
            m_currentBattery -= 10;
            //電力が減った後のゲージの見た目を更新
            m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
        }

    }

    //アイテムで電力を回復
    public void HealBattery()
    {

        m_currentBattery += 50;
        //電力が回復した後のゲージの見た目を更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }
}

