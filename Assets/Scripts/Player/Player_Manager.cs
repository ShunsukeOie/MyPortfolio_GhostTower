using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    // 移動の処理をまとめたスクリプト
    private Player_Move m_moveComp;

    // ライトの処理をまとめたスクリプト
    private Player_Light m_lightComp;

    // ランタンを灯す処理をまとめたスクリプト
    private Player_lantern m_lanternComp;

    // スタート位置格納用
    private Vector3 m_startPos;

    void Start()
    {
        // コンポーネントを取得
        m_moveComp = GetComponent<Player_Move>();
        m_lightComp = GetComponent<Player_Light>();
        m_lanternComp = GetComponent<Player_lantern>();

        // スタート位置を格納する
        m_startPos = transform.position;
    }

    void Update()
    {
        m_moveComp.PlayerMove();
        m_moveComp.PlayerRotate();

        //目の前にランタンがあるかをチェック
        m_lanternComp.LanternCheck();



        if (Input.GetButtonDown("LightUp"))
        {
            // 光を灯す
            m_lanternComp.LightUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            // タグがEnemyの場合
            case "Enemy":
                // スタート位置に戻す
                transform.position = m_startPos;

                // やられ音を流す
                AudioManager.instance.PlayerDeadSE();
                break;

            // タグがItemの場合
            case "Item":
                // アイテムゲットの音を流す
                AudioManager.instance.ItemGetSE();

                // バッテリーを回復する
                m_lightComp.HealBattery();

                // アイテムを削除
                Destroy(other.gameObject);
                break;
        }
    }
}
