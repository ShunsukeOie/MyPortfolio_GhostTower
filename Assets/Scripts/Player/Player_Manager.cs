using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    //ライトの処理をアタッチ
    private Player_Light m_lightScript;


    // スタート位置格納用
    private Vector3 m_startPos;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        m_lightScript = GetComponent<Player_Light>();

        // スタート位置を格納する
        m_startPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // タグがEnemyのオブジェクトに当たったら処理する
        if (other.gameObject.tag == "Enemy")
        {
            // スタート位置に戻す
            transform.position = m_startPos;

            // やられ音を流す
            AudioManager.instance.PlayerDeadSE();
        }

        // タグがItemのオブジェクトに当たったら処理する
        if (other.gameObject.tag == "Item")
        {
            // アイテムゲットの音を流す
            AudioManager.instance.ItemGetSE();

            // バッテリーを回復する
            m_lightScript.HealBattery();

            // アイテムを削除
            Destroy(other.gameObject);
        }
    }
}
