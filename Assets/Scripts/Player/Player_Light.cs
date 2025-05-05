using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    [SerializeField, Header("ライトオブジェクト")]
    private GameObject m_lightObj;

    [SerializeField, Header("ゲージ操作クラス")]
    private GaugeController m_gaugeController;

    [SerializeField, Header("光の点滅用カーブ")]
    private AnimationCurve m_curve;

    [SerializeField, Header("フラッシュのインターバル")]
    private float m_useFlashInterval;

    [SerializeField, Header("光の点滅スピード")]
    private float m_blinkingSpeed;

    // ライトのスクリプトを格納する変数
    private Light m_lightComp;

    // フラッシュ判定用のスクリプト格納用
    private Player_Vision m_flashJudgeComp;

    // コルーチンが多重起動起動しないようにするための変数
    private Coroutine m_coroutine;

    // 現在の電力
    private float m_currentBattery = 100f;

    // 最初の光の値
    private float m_startIntensity;

    // フラッシュのクールタイマー
    private float m_flashCoolTimer;

    // 光の点滅用タイマー
    private float m_blinkingTimer = 0f;

    // ライトが点灯しているか判定するフラグ
    private bool m_isLighting = false;

    // バッテリーがあるかを判定するフラグ
    private bool m_isBatteryDepleted = false;

    // 定数
    private const float MAX_BATTERY = 100f;         //最大電力
    private const float FLASH_BATTERY_COST = 10f;   // フラッシュで使うバッテリーのコスト
    private const float HEAL_BATTERY_VALUE = 50f;   // 回復するバッテリーの量

    void Awake()
    {
        // スクリプトを取得する
        m_flashJudgeComp = GetComponent<Player_Vision>();
        m_lightComp = m_lightObj.GetComponent<Light>();

        // 最初の光の値を取得
        m_startIntensity = m_lightComp.intensity;

        // ライトオブジェクトを非アクティブにしておく
        m_lightObj.SetActive(false);

        //ゲージを初期値で更新
        m_gaugeController.UpdateGauge(m_currentBattery, MAX_BATTERY);
    }

    void Update()
    {
        // バッテリーの残量を更新
        UpdateBattery();

        // ライトを点灯する
        LightUp();

        // バッテリーの残量が10以上ならフラッシュ出来る
        if (m_currentBattery >= FLASH_BATTERY_COST)
        {
            Flash();
        }

        // バッテリーの残量でゲージの色を変更する
        UpdateGaugeColor();
    }

    // ライトを点灯させる関数
    void LightUp()
    {
        if (m_currentBattery <= 0f) return;

        // ライトが点灯していなかったら処理する
        if (Input.GetButtonDown("Light") && !m_isLighting)
        {
            // 点灯しているかのフラグを上げる
            m_isLighting = true;
            // ライトオブジェクトをアクティブにする
            m_lightObj.SetActive(true);
            // ライト点灯の音を鳴らす
            AudioManager.Instance.LightUpSE();
        }

        // ライトが点灯していたら処理する
        else if (Input.GetButtonDown("Light") && m_isLighting)
        {
            // 点灯しているかのフラグを下げる
            m_isLighting = false;
            // ライトオブジェクトを非アクティブにする
            m_lightObj.SetActive(false);
            // ライト消灯の音を鳴らす
            AudioManager.Instance.LightUpSE();
        }
    }

    // フラッシュ用関数
    void Flash()
    {
        // 入力制限用
        if (m_flashCoolTimer >= 0f)
        {
            m_flashCoolTimer -= Time.deltaTime;
        }

        // ボタンが押されたかつライトが付いているかつタイマーが0以下かつバッテリーがある場合処理する
        if (Input.GetButtonDown("Flash") && m_isLighting && m_flashCoolTimer <= 0.0f && m_currentBattery > 0)
        {
            // フラッシュ判定用オブジェクトから敵リストを取得
            List<Enemy> targets = m_flashJudgeComp.GetEnemies();

            foreach (var enemy in targets)
            {
                // スタンを付与
                enemy.SetStan(true);
            }

            // 光量を上げる
            m_lightComp.intensity = 100f;

            // もし複数回コルーチンが呼ばれることがあったら古いコルーチンを止める
            if(m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
            }

            // 光量を下げていくコルーチンスタート
            m_coroutine = StartCoroutine(DecreaseLightIntensity());

            // タイマーをリセットする
            m_flashCoolTimer = m_useFlashInterval;

            //バッテリーを減少させる
            BatteryFlash();

            // フラッシュの音を鳴らす
            AudioManager.Instance.FlashSE();
        }
    }

    // バッテリーの残量でゲージの色を変更する関数
    void UpdateGaugeColor()
    {
        if (m_currentBattery >= 50f)
        {
            //緑
            m_gaugeController.ChangeColor1();
        }
        else if (m_currentBattery <= 50.0f && m_currentBattery >= 20f)
        {
            //黄
            m_gaugeController.ChangeColor2();
        }
        else
        {
            //赤
            m_gaugeController.ChangeColor3();
        }
    }

    // 光量を下げるコルーチン
    IEnumerator DecreaseLightIntensity()
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
            m_lightComp.intensity = intensity;
        }
        // 状態をクリア
        m_coroutine = null;
    }

    void UpdateBattery()
    {
        if (m_currentBattery > 0 && m_isLighting)
        {
            float newBattery = m_currentBattery -= Time.deltaTime;
            //バッテリーを減らす
            SetBattery(newBattery);

            // バッテリーが50以下になったら
            if (m_currentBattery <= 50f)
            {
                // カーブを使用してライトがチカチカする表現をする
                m_blinkingTimer += Time.deltaTime;
                m_lightComp.intensity = m_startIntensity * m_curve.Evaluate(m_blinkingTimer * m_blinkingSpeed);
            }
        }

        //　バッテリーが0になったらライトを消す
        if (m_currentBattery <= 0 && !m_isBatteryDepleted)
        {
            m_isBatteryDepleted = true;
            // ライトオブジェクトを非アクティブにする
            m_lightObj.SetActive(false);
            // ライト消灯の音を鳴らす
            AudioManager.Instance.LightUpSE();
        }
    }

    //電力をフラッシュライトで消費
    public void BatteryFlash()
    {
        float newBattery = m_currentBattery -= FLASH_BATTERY_COST;
        // バッテリーを0～MAXの値で補正しつつ、ゲージを更新する
        SetBattery(newBattery);
    }

    //アイテムで電力を回復
    public void HealBattery()
    {
        float newBattery = m_currentBattery += HEAL_BATTERY_VALUE;
        // バッテリーを0～MAXの値で補正しつつ、ゲージを更新する
        SetBattery(newBattery);

        // 0以下の時にバッテリーが回復されたら
        if(m_currentBattery < 0)
        {
            // バッテリーがあるか判定するフラグを上げる
            m_isBatteryDepleted = false;
        }
    }

    // バッテリーを0～MAXの値で補正しつつ、ゲージを更新する関数
    void SetBattery(float batteryValue)
    {
        m_currentBattery = Mathf.Clamp(batteryValue, 0f, MAX_BATTERY);
        m_gaugeController.UpdateGauge(m_currentBattery, MAX_BATTERY);
    }
}

