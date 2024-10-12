using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{
    // 光るかどうかのフラグ
    [HideInInspector]
    public bool isLighting = false;
    
    // Lightのスクリプト格納用
    [SerializeField]
    private Light m_lightscript;

    void Start()
    {
        // コンポーネントを取得する
        m_lightscript = GetComponent<Light>();
    }

    void Update()
    {
        // 光状態だったら
       if(isLighting)
       {
            // 光量を上げる
            m_lightscript.intensity = 5f;

            // 光量を下げていくコルーチンスタート
            StartCoroutine(Downintensity());
            isLighting = false;
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

        for (float intensity = 5f; intensity >= 0.0f; intensity -= 0.1f)
        {
            // 待ち時間
            yield return new WaitForSeconds(waittime);

            // 光量を徐々に下げていく
            m_lightscript.intensity = intensity;
        }
    }
}
