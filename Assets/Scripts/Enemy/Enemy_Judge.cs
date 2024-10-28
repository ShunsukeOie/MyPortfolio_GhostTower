using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Judge : MonoBehaviour
{
    // 見える範囲
    [SerializeField, Header("見える範囲")]
    private float angle = 45.0f;

    // エネミーのオブジェクト
    [SerializeField]
    private GameObject EnemyObj;
    // エネミーのオブジェクトのスクリプト取得用
    private Enemy_Search _search;

    // Start is called before the first frame update
    void Start()
    {
        // スクリプト取得
        _search = EnemyObj.GetComponent<Enemy_Search>();
    }

    // 範囲内に入っていたら
    private void OnTriggerStay(Collider other)
    {
        // エネミーがスタン状態ではないとき
        if (_search.isStan == false)
        {
            // タグがプレイヤーか判別する
            if (other.gameObject.tag == "Player")
            {
                // 正面に対して、プレイヤーの位置を取得し、45度以内か算出
                Vector3 posDelta = other.transform.position - this.transform.position;
                // Angle()関数で正面に対して何度の角度かを取得する
                float target_angle = Vector3.Angle(this.transform.forward, posDelta);

                // target_angleがm_angleに収まっているかどうか
                if (target_angle < angle)
                {
                    // レイを使用してPlayerに当たっているか判別する
                    if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                    {
                        // レイに当たったのがプレイヤーだったら処理する
                        if (hit.collider == other)
                        {
                            // プレイヤーを見つけたら早くなる
                            _search._agent.speed = 3.0f;
                            _search.isChasePlayer = true;

                            // 敵が追っているとき用の音に切り替える為、AudioManagerのフラグを変える
                            AudioManager mng = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                            mng.ChangeAudio = true;
                        }
                    }
                }
                // 敵がプレイヤーを追わなくする
                else
                {
                    // フラグを降ろす
                    _search.isChasePlayer = false;
                    // 敵をプレイヤーを追う前の目的地に向かって動かす
                    _search._agent.destination = _search.goals[_search.destNum].position;

                    // デフォルトの音に切り替える為、AudioManagerのフラグを変える
                    AudioManager mng = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                    mng.ChangeAudio = false;
                }
            }
        }
    }
}
