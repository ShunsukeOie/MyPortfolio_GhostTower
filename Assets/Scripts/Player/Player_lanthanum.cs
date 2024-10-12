using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_lanthanum : MonoBehaviour
{
    // 目の前にあるランタンを格納する為の変数
    private GameObject LanthanumObj = null;

    RaycastHit hit;
    [SerializeField, Header("Rayのサイズ")]
    private Vector3 BoxSize;

    [SerializeField, Header("どの距離まで判定を取るか")]
    private float Distance;


    [Header("どのレイヤーの判定を取るか")]
    public LayerMask layerMask;
    [SerializeField, Header("目の前にアイテムがある時に表示されるUI")]
    private GameObject UIImage;
    // アイテムが目の前にあるか
    private bool IsItem = false;

    // Update is called once per frame
    void Update()
    {
        //目の前にアイテムがあるかをチェック
        ItemCheck();

        // 光を灯す
        LightUp();
    }

    void ItemCheck()
    {
        // 目の前に箱型のレイを飛ばしてランタンがあるか判定する
        if(Physics.BoxCast(transform.position, BoxSize, transform.forward,
            out hit, gameObject.transform.rotation, Distance, layerMask))
        {
            // レイにヒットしたオブジェクトのタグがLanthanumだったら処理する
            if (hit.collider.gameObject.tag == "Lanthanum")
            {
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

    void LightUp()
    {
        //　Fキーで取得
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(LanthanumObj != null)
            {
                LanthanumObj.transform.Find("candle_").gameObject.GetComponent<Light>().enabled = true;
                LanthanumObj.gameObject.tag = "none";
            }
        }
    }
}
