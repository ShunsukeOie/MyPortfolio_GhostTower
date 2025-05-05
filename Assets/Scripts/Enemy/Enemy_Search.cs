using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy_Search : MonoBehaviour
{
    // 自身のスタート位置を格納する変数
    private Vector3 m_startPos;

    // プレイヤーを格納する変数
    private GameObject m_player;

    // 配列のインデックス番号指定用変数
    private int m_currentIndex = 0;

    // 目的地を設定する変数
    [SerializeField, Header("目的地")]
    private Transform[] m_patrolPoints = null;

    // ナビゲーションのコンポーネントを格納する変数
    private NavMeshAgent m_agent;

    void Awake()
    {
        // プレイヤーを取得
        m_player = GameObject.FindWithTag("Player");
        // スタート位置を保存する
        m_startPos = transform.position;
        // コンポーネント取得
        m_agent = GetComponent<NavMeshAgent>();
    }

    public void StartPatrol()
    {
        // 敵を次の目的地に向かって動かす
        m_agent.destination = m_patrolPoints[m_currentIndex].position;
    }

    // 次の目的地に移動する関数
    void UpdatePatrolPoint()
    {
        // インデックス番号を更新する（最終番号になったら最初の番号に戻す）
        m_currentIndex = (m_currentIndex + 1) % m_patrolPoints.Length;

        // 敵を次の目的地に向かって動かす
        m_agent.destination = m_patrolPoints[m_currentIndex].position;
    }

    // 敵の移動用関数
    public void UpdateMove(bool isChase)
    {
        // プレイヤーを追うフラグが立っていたら
        if (isChase)
        {
            // 移動速度を上げる
            m_agent.speed = 3.0f;
            // プレイヤーの位置に向かって移動する
            m_agent.SetDestination(m_player.transform.position);
        }
        else
        {
            // 移動速度を戻す
            m_agent.speed = 2.0f;
            // m_agent.remainingDistanceは敵と次の目的地までの距離を表している
            // 近づくほど0に近づいていく
            if (m_agent.remainingDistance < 0.5f)
            {
                // 次の目的地に向かう
                UpdatePatrolPoint();
            }
        }
    }

    // 敵のスタンの処理をまとめた関数
    public void HandleStan(bool isStan)
    {
        if (isStan)
        {
            // 移動速度を0にする
            m_agent.speed = 0.0f;
        }
        else
        {
            // 移動速度を戻す
            m_agent.speed = 2.0f;
        }
    }

    // プレイヤーに当たった際に初期化する関数
    public void ResetPosition()
    {
        // 初期位置にリセット
        transform.position = m_startPos;

        // 敵を最初の目的地に向かって動かす
        m_currentIndex = 0;
        m_agent.destination = m_patrolPoints[m_currentIndex].position;

        // 移動速度を戻す
        m_agent.speed = 2.0f;
    }
}
