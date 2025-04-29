using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    // ライトオブジェクト
    [SerializeField, Header("ライトオブジェクト")]
    private GameObject m_lightObj;
    // ライトのスクリプトを格納する変数
    private Light m_lightScript;

    // フラッシュを判定するオブジェクト
    //（別にすることで敵の索敵範囲との干渉をなくす）
    [SerializeField, Header("フラッシュ判定オブジェクト")]
    private GameObject m_judgeObj;
    // フラッシュ判定用のスクリプト格納用
    private Flash_Judge m_JudgeScript;

    //ゲージ操作クラスの取得用変数
    [SerializeField, Header("ゲージ操作クラス")]
    private GaugeController m_gaugeController;

    // フラッシュのインターバル用の変数
    [SerializeField, Header("フラッシュのインターバル")]
    private float m_useFlashInterval;
    private float m_flashCoolTimer;

    //バッテリー用
    private float m_maxBattery = 100;     //最大電力
    private float m_currentBattery = 100; //現在の電力

    [SerializeField, Header("光の点滅用カーブ")]
    AnimationCurve m_curve;
    [SerializeField, Header("光の点滅スピード")]
    private float m_blinkingSpeed;
    // 最初の光の値
    private float m_startIntensity;
    // 光の点滅用タイマー
    float m_blinkingTimer = 0f;

    // ライトが点灯しているか判定するフラグ
    private bool m_isLighting;

    void Start()
    {
        // スクリプトを取得する
        m_lightScript = m_lightObj.GetComponent<Light>();
        m_JudgeScript = m_judgeObj.GetComponent<Flash_Judge>();


        // フラグを初期化
        m_isLighting = false;
        // ライトオブジェクトを非アクティブにしておく
        m_lightObj.SetActive(false);
        // 最初の光の値を取得
        m_startIntensity = m_lightScript.intensity;

        //ゲージを初期値で更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);

    }

    void Update()
    {

        //　バッテリーが0になったらライトを消す
        if (m_currentBattery <= 0)
        {
            // ライトオブジェクトを非アクティブにする
            m_lightObj.SetActive(false);

            // ライト消灯の音を鳴らす
            AudioManager.instance.LightUpSE();
        }

        // ライトを点灯する
        LightUp();

        // バッテリーの残量が10以上ならフラッシュする
        if (m_currentBattery >= 10)
        {
            Flash();
        }

        // ライトが点灯していたら処理する
        if (m_isLighting == true)
        {
            // バッテリーを時間経過で減らしていく
            BatteryDecrease();
        }


        //バッテリーの残量でゲージの色を変更する
        if (m_currentBattery >= 50)
        {
            //緑
            m_gaugeController.ChangeColor1();
        }
        else if (m_currentBattery <= 50 && m_currentBattery >= 20)
        {
            //黄
            m_gaugeController.ChangeColor2();
        }
        else
        {
            //赤
            m_gaugeController.ChangeColor3();

        }

        if(m_currentBattery <= 50)
        {
            m_blinkingTimer += Time.deltaTime;
            m_lightScript.intensity = m_startIntensity * m_curve.Evaluate(m_blinkingTimer * m_blinkingSpeed);
        }
    }

    // ライトを点灯させる関数
    void LightUp()
    {

        // ライトが点灯していなかったら処理する
        if (Input.GetButtonDown("Light") && !m_isLighting && m_currentBattery > 0)
        {
            // 点灯しているかのフラグを上げる
            m_isLighting = true;
            // ライトオブジェクトをアクティブにする
            m_lightObj.SetActive(true);
            // ライト点灯の音を鳴らす
            AudioManager.instance.LightUpSE();
        }

        // ライトが点灯していたら処理する
        else if (Input.GetButtonDown("Light") && m_isLighting)
        {
            // 点灯しているかのフラグを下げる
            m_isLighting = false;
            // ライトオブジェクトを非アクティブにする
            m_lightObj.SetActive(false);
            // ライト消灯の音を鳴らす
            AudioManager.instance.LightUpSE();
        }
    }

    // フラッシュ用関数
    void Flash()
    {
        // 入力制限用
        if (m_flashCoolTimer >= 0.0f)
        {
            m_flashCoolTimer -= Time.deltaTime;
        }

        // ボタンが押されたかつライトが付いているかつタイマーが0以下かつバッテリーがある場合処理する
        if (Input.GetButtonDown("Flash") && m_isLighting && m_flashCoolTimer <= 0.0f && m_currentBattery > 0)
        {
            // フラッシュ判定用オブジェクトから敵リストを取得
            List<Enemy> targets = m_JudgeScript.GetEnemies();

            foreach (var enemy in targets)
            {
                enemy.SetStan(true); // スタンを付与
            }

            // 光量を上げる
            m_lightScript.intensity = 100f;

            // 光量を下げていくコルーチンスタート
            StartCoroutine(Downintensity());

            // タイマーをリセットする
            m_flashCoolTimer = m_useFlashInterval;

            //バッテリーを減少させる
            BatteryFlash();

            // フラッシュの音を鳴らす
            AudioManager.instance.FlashSE();
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
            m_lightScript.intensity = intensity;
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

