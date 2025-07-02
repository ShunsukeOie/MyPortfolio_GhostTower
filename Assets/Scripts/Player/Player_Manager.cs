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
    private Player_Lantern m_lanternComp;

    private Player_Vision m_visionComp;

    // スタート位置格納用
    private Vector3 m_startPos;

    void Start()
    {
        // コンポーネントを取得
        m_moveComp = GetComponent<Player_Move>();
        m_lightComp = GetComponent<Player_Light>();
        m_lanternComp = GetComponent<Player_Lantern>();
        m_visionComp = GetComponent<Player_Vision>();

        // スタート位置を格納する
        m_startPos = transform.position;
    }

    void Update()
    {
        // 移動、回転処理
        m_moveComp.PlayerMove();
        m_moveComp.PlayerRotate();

        // ライトの更新処理
        // バッテリーの残量を更新
        m_lightComp.UpdateBattery();

        // ライトを点灯する
        m_lightComp.LightUp();

        // フラッシュ使用時の処理
        m_lightComp.Flash();

        // バッテリーの残量でゲージの色を変更する
        m_lightComp.UpdateGaugeColor();

        //目の前にランタンがあるかをチェック
        m_lanternComp.LanternCheck();

        // 敵が視界内にいるかチェック
        m_visionComp.DetectEemiesInView();

        // ランタンの光を灯す
        m_lanternComp.LightUp();
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
                AudioManager.Instance.PlayerDeadSE();
                break;

            // タグがItemの場合
            case "Item":
                // アイテムゲットの音を流す
                AudioManager.Instance.ItemGetSE();

                // バッテリーを回復する
                m_lightComp.HealBattery();

                // アイテムを削除
                Destroy(other.gameObject);
                break;
        }
    }
}
