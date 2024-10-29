using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Judge : MonoBehaviour
{
    // フラッシュの範囲
    [SerializeField, Header("フラッシュの範囲")]
    private float angle = 45.0f;

    // エネミーのスクリプト取得用変数
    [HideInInspector]
    public Enemy_Search _esearch = null;

    // プレイヤーのオブジェクト
    [SerializeField, Header("プレイヤーオブジェ")]
    private GameObject player;
    // プレイヤーのスクリプト格納用
    private Player_Light _plyLight;

    private void Start()
    {
        // スクリプト取得
        _plyLight = player.GetComponent<Player_Light>();
    }

    // 範囲内に入っていたら
    private void OnTriggerStay(Collider other)
    {
        // タグがエネミーか判別する
        if (other.gameObject.tag == "Enemy")
        {
            // 正面に対して、プレイヤーの位置を取得し、45度以内か算出
            Vector3 posDelta = other.transform.position - this.transform.position;
            // Angle()関数で正面に対して何度の角度かを取得する
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            // target_angleがm_angleに収まっているかどうか
            if (target_angle < angle)
            {
                // レイを使用してEnemyに当たっているか判別する
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // レイに当たったのがエネミーだったら処理する
                    if (hit.collider == other)
                    {
                        // 敵のスクリプト取得
                        _esearch = other.GetComponent<Enemy_Search>();
                        // プレイヤーのスクリプトにアクセスし、フラグを上げる
                        _plyLight.canStopEnemy = true;
                    }
                }
                Debug.DrawRay(transform.position, posDelta, Color.red);
            }
            else
            {
                // スクリプト格納用変数をnullにしておく
                _esearch = null;
                // 角度内にいなかったらフラグを降ろす
                _plyLight.canStopEnemy = false;
            }
        }
    }
}
