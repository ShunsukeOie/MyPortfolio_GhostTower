using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_lanthanum : MonoBehaviour
{
    [SerializeField]
    private GameObject ItemObj;

    RaycastHit hit;
    [SerializeField, Header("Rayのサイズ")]
    private Vector3 BoxSize;

    [SerializeField,  Header("どの位置からRayを飛ばすか")]
    private Vector3 AddPos;

    [SerializeField, Header("どの距離まで判定を取るか")]
    private float Distance = 2f;


    [Header("どのレイヤーの判定を取るか")]
    public LayerMask layerMask;
    [SerializeField, Header("目の前にアイテムがある時に表示されるUI")]
    private GameObject UIImage;
    // アイテムが目の前にあるか
    private bool IsItem = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //目の前にアイテムがあるかをチェック
        ItemCheck();

        //　Fキーで取得
        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }

    void ItemCheck()
    {
        //目の前に箱型のRayを飛ばして　アイテムがあるかを探す
        if (Physics.BoxCast(gameObject.transform.position + AddPos, BoxSize, transform.forward,
            out hit, gameObject.transform.rotation, Distance, layerMask))
        {
            //　目の前のアイテムの情報を格納
            ItemObj = hit.collider.gameObject;
            if (!IsItem)
            {
                //UIを表示
                UIImage.SetActive(true);
                IsItem = true;
            }
        }
        else
        {
            //目の前に無かったら
            //アイテム情報を削除
            ItemObj = null;
            if (IsItem)
            {
                //UIを非表示に
                UIImage.SetActive(false);
                IsItem = false;
            }
        }
    }
}
