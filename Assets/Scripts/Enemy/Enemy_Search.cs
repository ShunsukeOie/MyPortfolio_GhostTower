using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Search : MonoBehaviour
{
    // 見える範囲
    [SerializeField, Header("見える範囲")]
    private float m_angle = 45.0f;

    // 範囲内に入っていたら
    private void OnTriggerStay(Collider other)
    {
        // タグがプレイヤーか判別する
        if(other.gameObject.tag == "Player")
        {
            // 正面に対して、プレイヤーの位置を取得し、45度以内か算出
            Vector3 posDelta = other.transform.position - this.transform.position;
            // Angle()関数で正面に対して何度の角度かを取得する
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            // target_angleがm_angleに収まっているかどうか
            if(target_angle < m_angle)
            {
                // レイを使用してtargeに当たっているか判別する
                if(Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // レイに当たったのがプレイヤーだったら処理する
                    if(hit.collider == other)
                    {
                        Debug.Log("見えている");
                    }
                }
            }
        }
    }
}
