using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{
    // Lightのスクリプト格納用
    private Light _lightscript;

    // Enemy_Searchスクリプト格納用
    private Enemy_Search _searchscript;

    // 光るかどうかのフラグ
    [HideInInspector]
    public bool isLighting = false;
    
    void Start()
    {
        // コンポーネントを取得する
        _lightscript = GetComponent<Light>();
        _searchscript = GetComponent<Enemy_Search>();

    }

    void Update()
    {
        // 敵がプレイヤーを追っていたら処理する
        if(_searchscript.isChasePlayer)
        {
            // 敵を光らせる
            _lightscript.intensity = 50f;
        }
        else
        {
            // 光を消す
            _lightscript.intensity = 0f;
        }

        // 光状態だったら
       if(isLighting)
       {
            // 光量を上げる
            _lightscript.intensity = 50f;

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
        float downtime = 0.2f;
        // ウェイト時間算出
        float waittime = downtime / loopcount;

        for (float intensity = 50f; intensity >= 0.0f; intensity -= 0.25f)
        {
            // 待ち時間
            yield return new WaitForSeconds(waittime);

            // 光量を徐々に下げていく
            _lightscript.intensity = intensity;
        }
    }
}
