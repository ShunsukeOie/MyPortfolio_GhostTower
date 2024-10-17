using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 制限時間用変数
    [SerializeField, Header("制限時間")]
    private float countdown;

    // 時間を表示するテキスト格納用
    public Text timeText;

    // Update is called once per frame
    void Update()
    {
        // 時間を減らしていく
        countdown -= Time.deltaTime;

        // テキストに時間を反映させる（引数で小数点を調整できる）
        timeText.text = countdown.ToString("F0");

        // 時間が0以下になったら
        if(countdown <= 0)
        {
            // ゲームオーバーのシーンをロードする
            SceneManager.LoadScene("GameOver");
        }
    }
}
