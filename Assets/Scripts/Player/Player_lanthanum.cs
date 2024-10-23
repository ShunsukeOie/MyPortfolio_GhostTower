using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_lanthanum : MonoBehaviour
{
    // 目の前にあるランタンを格納する為の変数
    private GameObject LanthanumObj = null;

    [SerializeField, Header("目の前にアイテムがある時に表示されるUI")]
    private GameObject UIImage;

    [SerializeField, Header("Rayのサイズ")]
    private Vector3 BoxSize;

    // レイの判定用
    private RaycastHit hit;

    [SerializeField, Header("どの距離まで判定を取るか")]
    private float Distance;
    
    // アイテムが目の前にあるか
    private bool IsItem = false;

    [Header("どのレイヤーの判定を取るか")]
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        //目の前にランタンがあるかをチェック
        LanthanumCheck();

        // 光を灯す
        LightUp();
    }

    // 目の前にランタンがあるかチェックする関数
    void LanthanumCheck()
    {
        // 目の前に箱型のレイを飛ばしてランタンがあるか判定する
        if(Physics.BoxCast(transform.position, BoxSize, transform.forward,
            out hit, gameObject.transform.rotation, Distance, layerMask))
        {
            // レイにヒットしたオブジェクトのタグがLanthanumだったら処理する
            if (hit.collider.gameObject.tag == "Lanthanum")
            {
                Debug.Log("ある");
                Debug.Log(hit.collider.gameObject.name);
                //　目の前のランタンの情報を格納
                LanthanumObj = hit.collider.gameObject;

                if (!IsItem)
                {
                    //UIを表示
                    UIImage.SetActive(true);
                    IsItem = true;
                }
            }
        }
        else
        {
            //目の前に無かったら
            //アイテム情報を削除
            LanthanumObj = null;
            if (IsItem)
            {
                //UIを非表示に
                UIImage.SetActive(false);
                IsItem = false;
            }
        }
    }

    // ランタンを点灯させる関数
    void LightUp()
    {
        if (Input.GetButtonDown("LightUp"))
        {
            // ランタンオブジェがnullじゃなかったら処理する
            if(LanthanumObj != null)
            {
                // ランタンオブジェの子オブジェクトを取得する
                GameObject candle = LanthanumObj.transform.Find("candle_").gameObject;
                // 点滅をやめる
                candle.GetComponent<FlickeringLight>().enabled = false;
                // 光の範囲を6に固定する
                candle.GetComponent<Light>().range = 6;
                // ランタンオブジェクトが反応しないようタグを切り替えておく
                LanthanumObj.gameObject.tag = "none";
                // UIを非表示にする
                UIImage.SetActive(false);
            }
        }
    }
}
