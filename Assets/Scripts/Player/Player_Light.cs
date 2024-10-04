using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player_Light : MonoBehaviour
{
    [SerializeField, Header("ライトオブジェクト")]
    private GameObject LightObj;

    // ライトが点灯しているか判定するフラグ
    private bool m_isLighting;

    private float FlashCoolTimer;
    private float FlashCoolTime;

    // ライトのスクリプトを格納する変数
    Light m_lightscript;

    void Start()
    {
        // スクリプトを取得する
        m_lightscript = LightObj.GetComponent<Light>();
        // フラグを初期化
        m_isLighting = false;
        // ライトオブジェクトを非アクティブにしておく
        LightObj.SetActive(false);
    }

    void Update()
    {
        // ライトを点灯する
        LightUp();

        if (Input.GetButtonDown("Flash") && m_isLighting)
        { 
           // 光量を上げる
            m_lightscript.intensity = 10f;

            // 光量を下げていくコルーチンスタート
            StartCoroutine(Downintensity());
        }
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
            LightObj.SetActive(true);
        }

        // ライトが点灯していたら処理する
        else if (Input.GetButtonDown("Light") && m_isLighting)
        {
            // 点灯しているかのフラグを下げる
            m_isLighting = false;
            // ライトオブジェクトを非アクティブにする
            LightObj.SetActive(false);
        }
    }

    // 光量を下げるコルーチン
    IEnumerator Downintensity()
    {
        // ループ回数(値を増やすと滑らかになる)
        int loopcount = 50;
        // 下がりきるまでにかかる時間
        float downtime = 1.0f;        
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
