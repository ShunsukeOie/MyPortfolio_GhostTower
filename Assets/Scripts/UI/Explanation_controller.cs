using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explanation_controller : MonoBehaviour
{
    //変更するオブジェクト格納（操作説明）
    [SerializeField] protected Image _ExplanationImage1;

    //変更するオブジェクト格納（遊び方）
    [SerializeField] protected Image _ExplanationImage2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 画面をフェードインさせるコールチン
    public IEnumerator ChangeColor1()
    {
        // フェード後の色を設定（黒）★変更可
        _ExplanationImage1.color = new Color((255.0f / 255.0f), (255.0f / 255.0f), (255.0f / 255.0f), (0.0f / 255.0f));

        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 0.5f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 10;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = _ExplanationImage1.color;
            new_color.a = alpha / 255.0f;
            _ExplanationImage1.color = new_color;

        }
    }
    public IEnumerator ChangeColor2()
    {
        // フェード後の色を設定（黒）★変更可
        _ExplanationImage2.color = new Color((255.0f / 255.0f), (255.0f / 255.0f), (255.0f / 255.0f), (0.0f / 255.0f));

        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 0.5f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 10;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = _ExplanationImage2.color;
            new_color.a = alpha / 255.0f;
            _ExplanationImage2.color = new_color;

        }
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
