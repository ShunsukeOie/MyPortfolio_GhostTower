using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy_Search : MonoBehaviour
{
    // 自身のスタート位置を格納する変数
    private Vector3 m_startPos;

    // 目的地を設定する変数
    [SerializeField, Header("目的地")]
    private Transform[] m_goals = null;
    // ナビゲーションのコンポーネントを格納する変数
    private NavMeshAgent m_agent;
    // 配列のインデックス番号指定用変数
    private int m_destNum = 0;

    // プレイヤーの位置を格納する変数
    [SerializeField]
    private Transform m_player;

    // 見える範囲
    [SerializeField, Header("見える範囲")]
    private float m_angle = 45.0f;

    // スタン時間計測用
    private float m_stanTime = 1.0f;
    private float m_stanTimer = 0.0f;

    // スタン状態かどうかのフラグ
    [HideInInspector]
    public bool isStan = false;

    void Start()
    {
        // スタート位置を保存する
        m_startPos = transform.position;
        // コンポーネント取得
        m_agent = GetComponent<NavMeshAgent>();
        // 敵を次の目的地に向かって動かす
        m_agent.destination = m_goals[m_destNum].position;
    }

    void Update()
    {
        // スタン状態だったら（プレイヤーからライトを食らったらtrueになる）
        if(isStan)
        {
            // 移動速度を0にする
            m_agent.speed = 0.0f;

            // 時間を加算する
            m_stanTimer += Time.deltaTime;
            // タイマーがスタン時間を超えたら
            if(m_stanTimer >= m_stanTime)
            {
                // スピードを元に戻す
                m_agent.speed = 2.0f;
                // 時間をリセットする
                m_stanTimer = 0.0f;
                // フラグを降ろす
                isStan = false;
            }
        }
        // スタン状態じゃない
        else
        {
            // m_agent.remainingDistanceは敵と次の目的地までの距離を表している
            // 近づくほど0に近づいていく
            if (m_agent.remainingDistance < 0.5f)
            {
                // プレイヤーを見つけてないときの速度
                m_agent.speed = 2.0f;
                // 次の目的地に向かう
                nextGoal();
            }
        }
    }

    // 次の目的地に移動する関数
    void nextGoal()
    {
        // インデックス番号を更新する
        m_destNum += 1;
        // 最終番号になったら
        if (m_destNum == 3)
        {
            // 最初の番号に戻す
            m_destNum = 0;
        }
        // 敵を次の目的地に向かって動かす
        m_agent.destination = m_goals[m_destNum].position;

        Debug.Log(m_destNum);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに触れたら処理する
        if(collision.gameObject.tag == "Player")
        {
            // 元の位置に戻る
            transform.position = m_startPos;
        }
    }


    // 範囲内に入っていたら
    private void OnTriggerStay(Collider other)
    {
        if(!isStan)
        {
            // タグがプレイヤーか判別する
            if (other.gameObject.tag == "Player")
            {
                // 正面に対して、プレイヤーの位置を取得し、45度以内か算出
                Vector3 posDelta = other.transform.position - this.transform.position;
                // Angle()関数で正面に対して何度の角度かを取得する
                float target_angle = Vector3.Angle(this.transform.forward, posDelta);

                // target_angleがm_angleに収まっているかどうか
                if (target_angle < m_angle)
                {
                    // レイを使用してtargeに当たっているか判別する
                    if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                    {
                        // レイに当たったのがプレイヤーだったら処理する
                        if (hit.collider == other)
                        {
                            // プレイヤーを見つけたら早くなる
                            m_agent.speed = 3.5f;
                            // プレイヤーの位置に向かって移動する
                            m_agent.SetDestination(m_player.position);
                            Debug.Log("見えている");
                        }
                    }
                }
            }
        }       
    }
}
