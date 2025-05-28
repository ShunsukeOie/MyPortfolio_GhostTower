using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Lantern : MonoBehaviour
{
    // 目の前にあるランタンを格納する為の変数
    private GameObject m_lanternObj = null;

    [SerializeField, Header("目の前にアイテムがある時に表示されるUI")]
    private GameObject m_UIImage;

    [SerializeField, Header("Rayのサイズ")]
    private Vector3 m_BoxSize = new Vector3(0.25f, 0.25f, 0.25f);

    // レイの判定用
    private RaycastHit m_hit;

    [SerializeField, Header("どの距離まで判定を取るか")]
    private float m_distance = 1.0f;
    
    // アイテムが目の前にあるか
    private bool m_isItem = false;

    [SerializeField, Header("どのレイヤーの判定を取るか")]
    private LayerMask m_layerMask;

    // 目の前にランタンがあるかチェックする関数
    public void LanternCheck()
    {
        // 目の前に箱型のレイを飛ばしてランタンがあるか判定する
        if(Physics.BoxCast(transform.position, m_BoxSize, transform.forward,
            out m_hit, gameObject.transform.rotation, m_distance, m_layerMask))
        {
            // レイにヒットしたオブジェクトのタグがLanternだったら処理する
            if (m_hit.collider.gameObject.tag == "Lantern")
            {
                //　目の前のランタンの情報を格納
                m_lanternObj = m_hit.collider.gameObject;

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
            m_lanternObj = null;
            if (m_isItem)
            {
                //UIを非表示に
                m_UIImage.SetActive(false);
                m_isItem = false;
            }
        }
    }

    // ランタンを点灯させる関数
    public void LightUp()
    {
        // ランタンオブジェがnullじゃなかったら処理する
        if (m_lanternObj != null)
        {
            // ランタンオブジェの子オブジェクトを取得する
            GameObject candle = m_lanternObj.transform.Find("candle_").gameObject;
            // 点滅をやめる
            candle.GetComponent<FlickeringLight>().enabled = false;
            // 光の範囲を6に固定する
            candle.GetComponent<Light>().range = 6;
            // ランタンオブジェクトが反応しないようレイヤーを変えておく
            m_lanternObj.layer = LayerMask.NameToLayer("Ignore Raycast");
            // UIを非表示にする
            m_UIImage.SetActive(false);

            // ランタン点灯の音を流す
            AudioManager.Instance.LanthanumSE();
        }
    }
}
