using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision : MonoBehaviour
{
    // エネミーのオブジェクト
    [SerializeField]
    private GameObject m_enemyObj;
    // エネミーのオブジェクトのスクリプト取得用
    private Enemy m_enemyComp;

    // 見える範囲
    [SerializeField, Header("見える範囲")]
    private float m_viewAngle = 45.0f;


    // Start is called before the first frame update
    void Start()
    {
        // エネミーを取得
        m_enemyObj = transform.parent.gameObject;
        // スクリプト取得
        m_enemyComp = m_enemyObj.GetComponent<Enemy>();
    }

    // 範囲内に入っていたら
    private void OnTriggerStay(Collider other)
    {
        // エネミーがスタン状態ではないかつタグがPlayerのとき
        if (other.gameObject.tag == "Player" && !m_enemyComp.m_isStan)
        {
            // 正面に対して、プレイヤーの位置を取得し、45度以内か算出
            Vector3 posDelta = other.transform.position - this.transform.position;
            // Angle()関数で正面に対して何度の角度かを取得する
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            // target_angleがm_angleに収まっているかどうか
            if (target_angle < m_viewAngle)
            {
                // レイを使用してPlayerに当たっているか判別する
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // レイに当たったのがプレイヤーだったら処理する
                    if (hit.collider == other)
                    {
                        m_enemyComp.SetChasePlayer(true);
                    }
                }
            }
            // 敵がプレイヤーを追わなくする
            else
            {
                // フラグを降ろす
                m_enemyComp.SetChasePlayer(false);
            }
        }
    }
}