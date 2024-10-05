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

    // ライトが点灯しているか判定するフラグ
    private bool m_isLighting;

    // フラッシュのインターバル用の変数
    [SerializeField]
    private float m_UseFlashInterval;
    private float m_FlashCoolTimer;
    
    // ライトのスクリプトを格納する変数
    Light m_lightscript;

    void Start()
    {
        // スクリプトを取得する
        m_lightscript = m_LightObj.GetComponent<Light>();
        // フラグを初期化
        m_isLighting = false;
        // ライトオブジェクトを非アクティブにしておく
        m_LightObj.SetActive(false);
    }

    void Update()
    {
        // ライトを点灯する
        LightUp();

        // フラッシュする
        Flash();
    }

    // ライトを点灯させる関数
    void LightUp()
    {
        // ライトが点灯していなかったら処理する
        if (Input.GetButtonDown("Light") && !m_isLighting)
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

        // ボタンが押されたかつライトが付いているかつタイマーが0以下の場合処理する
        if (Input.GetButtonDown("Flash") && m_isLighting && m_FlashCoolTimer <= 0.0f)
        {
            // 光量を上げる
            m_lightscript.intensity = 10f;

            // 光量を下げていくコルーチンスタート
            StartCoroutine(Downintensity());

            // タイマーをリセットする
            m_FlashCoolTimer = m_UseFlashInterval;
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
}
