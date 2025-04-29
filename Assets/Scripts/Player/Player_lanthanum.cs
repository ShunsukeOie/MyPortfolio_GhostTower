using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_lanthanum : MonoBehaviour
{
    // 目の前にあるランタンを格納する為の変数
    private GameObject m_lanthanumObj = null;

    [SerializeField, Header("目の前にアイテムがある時に表示されるUI")]
    private GameObject m_UIImage;

    [SerializeField, Header("Rayのサイズ")]
    private Vector3 m_BoxSize;

    // レイの判定用
    private RaycastHit m_hit;

    [SerializeField, Header("どの距離まで判定を取るか")]
    private float m_distance;
    
    // アイテムが目の前にあるか
    private bool m_isItem = false;

    [SerializeField, Header("どのレイヤーの判定を取るか")]
    private LayerMask m_layerMask;

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
        if(Physics.BoxCast(transform.position, m_BoxSize, transform.forward,
            out m_hit, gameObject.transform.rotation, m_distance, m_layerMask))
        {
            // レイにヒットしたオブジェクトのタグがLanthanumだったら処理する
            if (m_hit.collider.gameObject.tag == "Lanthanum")
            {
                //　目の前のランタンの情報を格納
                m_lanthanumObj = m_hit.collider.gameObject;

                if (!m_isItem)
                {
                    //UIを表示
                    m_UIImage.SetActive(true);
                    m_isItem = true;
                }
            }
        }
        else
        {
            //目の前に無かったら
            //アイテム情報を削除
            m_lanthanumObj = null;
            if (m_isItem)
            {
                //UIを非表示に
                m_UIImage.SetActive(false);
                m_isItem = false;
            }
        }
    }

    // ランタンを点灯させる関数
    void LightUp()
    {
        if (Input.GetButtonDown("LightUp"))
        {
            // ランタンオブジェがnullじゃなかったら処理する
            if(m_lanthanumObj != null)
            {
                // ランタンオブジェの子オブジェクトを取得する
                GameObject candle = m_lanthanumObj.transform.Find("candle_").gameObject;
                // 点滅をやめる
                candle.GetComponent<FlickeringLight>().enabled = false;
                // 光の範囲を6に固定する
                candle.GetComponent<Light>().range = 6;
                // ランタンオブジェクトが反応しないようタグを切り替えておく
                m_lanthanumObj.gameObject.tag = "none";
                // UIを非表示にする
                m_UIImage.SetActive(false);

                // ランタン点灯の音を流す
                AudioManager.instance.LanthanumSE();
            }
        }
    }
}
