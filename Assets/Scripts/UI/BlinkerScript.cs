using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkerScript : MonoBehaviour
{
    public float speed = 1.0f;
    private float time;
    private Text text;

    void Start()
    {
        //テキストの情報を取得
        text = this.gameObject.GetComponent<Text>();
    }

    void Update()
    {
        //点滅処理の呼び出し
        text.color = GetTextColorAlpha(text.color);
    }

    //テキストを点滅させる
    Color GetTextColorAlpha(Color color)
    {
        time += Time.deltaTime * speed * 5.0f;
        color.a = Mathf.Sin(time);

        return color;
    }
}
