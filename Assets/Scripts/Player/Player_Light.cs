using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    // ライトオブジェクト
    [SerializeField, Header("ライトオブジェクト")]
    private GameObject m_LightObj;

    // フラッシュの範囲
    [SerializeField, Header("フラッシュの範囲")]
    private float m_angle = 45.0f;

    [HideInInspector]
    public bool canStopEnemy = false;
    Enemy_Search esearch = null;

    // ライトが点灯しているか判定するフラグ
    private bool m_isLighting;

    // フラッシュのインターバル用の変数
    [SerializeField]
    private float m_UseFlashInterval;
    private float m_FlashCoolTimer;
    
    // ライトのスクリプトを格納する変数
    Light m_lightscript;

    //バッテリー用
    private float m_maxBattery = 100;     //最大電力
    private float m_currentBattery = 100; //現在の電力

    //ゲージ操作クラスの取得
    [SerializeField] private GaugeController m_gaugeController;

 
    void Start()
    {
        // スクリプトを取得する
        m_lightscript = m_LightObj.GetComponent<Light>();
        // フラグを初期化
        m_isLighting = false;
        // ライトオブジェクトを非アクティブにしておく
        m_LightObj.SetActive(false);

        //ゲージを初期値で更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);

    }

    void Update()
    {

        //　バッテリーが0になったらライトを消す
        if(m_currentBattery <= 0)
        {
            // ライトオブジェクトを非アクティブにする
            m_LightObj.SetActive(false);
        }

        // ライトを点灯する
        LightUp();
        
        // バッテリーの残量が10以上ならフラッシュする
        if(m_currentBattery >= 10)
        {
            Flash();
        }
        

        if(m_isLighting == true)
        {
            BatteryDecrease();
        }


        //バッテリーの残量でゲージの色を変更する
        if(m_currentBattery >= 50)
        {
            //緑
            m_gaugeController.ChangeColor1();
            //Debug.Log("1");
        }
        else if(m_currentBattery <= 50 && m_currentBattery >= 20)
        {
            //黄
            m_gaugeController.ChangeColor2();
            //Debug.Log("2");
        }
        else
        {
            //赤
            m_gaugeController.ChangeColor3();
            Debug.Log("3");
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
            m_LightObj.SetActive(true);

        }

        // ライトが点灯していたら処理する
        else if (Input.GetButtonDown("Light") && m_isLighting)
        {
            // 点灯しているかのフラグを下げる
            m_isLighting = false;
            // ライトオブジェクトを非アクティブにする
            m_LightObj.SetActive(false);

        }
    }

    // フラッシュ用関数
    void Flash()
    {
        // 入力制限用
        if (m_FlashCoolTimer >= 0.0f)
        {
            m_FlashCoolTimer -= Time.deltaTime;
        }

        // ボタンが押されたかつライトが付いているかつタイマーが0以下かつバッテリーがある場合処理する
        if (Input.GetButtonDown("Flash") && m_isLighting && m_FlashCoolTimer <= 0.0f && m_currentBattery > 0)
        {
            if(canStopEnemy)
            {
                if(esearch != null)
                {
                    esearch.isStan = true;
                }
            }
            // 光量を上げる
            m_lightscript.intensity = 10f;

            // 光量を下げていくコルーチンスタート
            StartCoroutine(Downintensity());

            // タイマーをリセットする
            m_FlashCoolTimer = m_UseFlashInterval;

            //バッテリーを減少させる
            BatteryFlash();
        }
    }

    // 光量を下げるコルーチン
    IEnumerator Downintensity()
    {
        // ループ回数(値を増やすと滑らかになる)
        int loopcount = 50;
        // 下がりきるまでにかかる時間
        float downtime = 0.7f;        
        // ウェイト時間算出
        float waittime = downtime / loopcount;

        for (float intensity = 10f; intensity >= 2.73f; intensity -= 0.1f)
        {
            // 待ち時間
            yield return new WaitForSeconds(waittime);

            // 光量を徐々に下げていく
            m_lightscript.intensity = intensity;
        }
    }

    // 範囲内に入っていたら
    private void OnTriggerStay(Collider other)
    {
        // タグがエネミーか判別する
        if (other.gameObject.tag == "Enemy")
        {
            // 正面に対して、プレイヤーの位置を取得し、45度以内か算出
            Vector3 posDelta = other.transform.position - this.transform.position;
            // Angle()関数で正面に対して何度の角度かを取得する
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            // target_angleがm_angleに収まっているかどうか
            if (target_angle < m_angle)
            {
                // レイを使用してtargeに当たっているか判別する
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // レイに当たったのがエネミーだったら処理する
                    if (hit.collider == other)
                    {
                        esearch = other.GetComponent<Enemy_Search>();
                        canStopEnemy = true;
                        Debug.Log("見えている");
                    }
                }
            }
            else
            {
                esearch = null;
                // 角度内にいなかったらフラグを降ろす
                canStopEnemy = false;
            }
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
        if(m_currentBattery >= 10)
        {
            m_currentBattery -= 10;
            //電力が減った後のゲージの見た目を更新
            m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
        }
        
    }

    //アイテムで電力を回復
    public void HealBattery()
    {
        m_currentBattery += 10;
        //電力が回復した後のゲージの見た目を更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }
}
