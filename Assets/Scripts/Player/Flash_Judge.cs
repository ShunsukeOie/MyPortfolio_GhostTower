using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Judge : MonoBehaviour
{
    // フラッシュの範囲
    [SerializeField, Header("フラッシュの範囲")]
    private float m_viewAngle = 45.0f;

    // エネミーのスクリプト取得用変数
    private List<Enemy> m_enemyList = new List<Enemy>();

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
            if (target_angle < m_viewAngle)
            {
                // レイを使用してEnemyに当たっているか判別する
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // レイに当たったのがエネミーだったら処理する
                    if (hit.collider == other)
                    {
                        // 敵のスクリプト取得
                        var enemy = other.GetComponent<Enemy>();
                        if(enemy != null && m_enemyList.Contains(enemy))
                        {
                            m_enemyList.Add(enemy);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // 敵のスクリプト取得
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null && m_enemyList.Contains(enemy))
            {
                m_enemyList.Remove(enemy);
            }
        }
    }

    public List<Enemy> GetEnemies()
    {
        return m_enemyList;
    }
}
