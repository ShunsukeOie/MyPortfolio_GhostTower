using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy_Search : MonoBehaviour
{
    // 自身のスタート位置を格納する変数
    private Vector3 startPos;

    // プレイヤーの位置を格納する変数
    [SerializeField]
    private Transform player;

    // 見える範囲
    [SerializeField, Header("見える範囲")]
    private float angle = 45.0f;

    // スタン時間計測用
    private float stanTime = 2.0f;
    private float stanTimer = 0.0f;

    // 目的地を設定する変数
    [Header("目的地")]
    public Transform[] goals = null;

    // ナビゲーションのコンポーネントを格納する変数
    [HideInInspector]
    public NavMeshAgent _agent;

    // 配列のインデックス番号指定用変数
    [HideInInspector]
    public int destNum = 0;

    // プレイヤーを追っているかを判定する変数
    [HideInInspector]
    public bool isChasePlayer = false;

    // スタン状態かどうかのフラグ
    [HideInInspector]
    public bool isStan = false;

    

    void Start()
    {
        // スタート位置を保存する
        startPos = transform.position;
        // コンポーネント取得
        _agent = GetComponent<NavMeshAgent>();
        // 敵を次の目的地に向かって動かす
        _agent.destination = goals[destNum].position;
    }

    void Update()
    {
        Move();
    }

    // 敵の移動用関数
    void Move()
    {
        // スタン状態だったら（プレイヤーからライトを食らったらtrueになる）
        if (isStan)
        {
            // 移動速度を0にする
            _agent.speed = 0.0f;

            // 時間を加算する
            stanTimer += Time.deltaTime;
            // タイマーがスタン時間を超えたら
            if (stanTimer >= stanTime)
            {
                // スピードを元に戻す
                _agent.speed = 2.0f;
                // 時間をリセットする
                stanTimer = 0.0f;
                // フラグを降ろす
                isStan = false;
            }
        }
        // スタン状態じゃない
        else
        {
            // m_agent.remainingDistanceは敵と次の目的地までの距離を表している
            // 近づくほど0に近づいていく
            if (_agent.remainingDistance < 0.5f && !isChasePlayer)
            {
                // プレイヤーを見つけてないときの速度
                _agent.speed = 2.0f;
                // 次の目的地に向かう
                nextGoal();
            }

            // プレイヤーを追うフラグが立っていたら
            if (isChasePlayer)
            {
                // プレイヤーの位置に向かって移動する
                _agent.SetDestination(player.position);
            }
        }
    }

    // 次の目的地に移動する関数
    void nextGoal()
    {
        // インデックス番号を更新する
        destNum += 1;
        // 最終番号になったら
        if (destNum == 8)
        {
            // 最初の番号に戻す
            destNum = 0;
        }
        // 敵を次の目的地に向かって動かす
        _agent.destination = goals[destNum].position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに触れたら処理する
        if(collision.gameObject.tag == "Player")
        {
            // 元の位置に戻る
            transform.position = startPos;

            // 敵を最初の目的地に向かって動かす
            destNum = 0;
            _agent.destination = goals[destNum].position;

            // プレイヤーを追わなくする
            isChasePlayer = false;

            // 速度を元に戻す
            _agent.speed = 2.0f;
        }
    }
}
