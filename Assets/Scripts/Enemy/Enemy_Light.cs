using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{
    // Lightのスクリプト格納用
    private Light m_lightScript;

    // 光るかどうかのフラグ
    private bool m_isLighting = false;

    // Enemy.csのStartの前に処理する
    void Awake()
    {
        // コンポーネントを取得する
        m_lightScript = GetComponent<Light>();
    }

    // プレイヤーを追っている時にエネミーを光らす関数
    public void UpdateLighting(bool isChase)
    {
        // 敵がプレイヤーを追っていたら処理する
        if (isChase)
        {
            // 敵を光らせる
            m_lightScript.intensity = 50.0f;
        }
        else
        {
            // 光を消す
            m_lightScript.intensity = 0.0f;
        }
    }

    // プレイヤーがアイテムを拾った時に敵を光らす関数
    public void FlashLight()
    {
        // 光っている状態だったら
        if (!m_isLighting)
        {
            m_isLighting=true;

            // 光量を上げる
            m_lightScript.intensity = 50.0f;

            // 光量を下げていくコルーチンスタート
            StartCoroutine(DecreaseLightIntensity());
            m_isLighting = false;
        }
    }

    // 光量を下げるコルーチン
    IEnumerator DecreaseLightIntensity()
    {
        // ループ回数(値を増やすと滑らかになる)
        int loopcount = 50;
        // 下がりきるまでにかかる時間
        float downtime = 0.2f;
        // ウェイト時間算出
        float waittime = downtime / loopcount;

        for (float intensity = 50f; intensity >= 0.0f; intensity -= 0.25f)
        {
            // 待ち時間
            yield return new WaitForSeconds(waittime);

            // 光量を徐々に下げていく
            m_lightScript.intensity = intensity;
        }
    }
}
