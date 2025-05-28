using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision : MonoBehaviour
{
    // 見える範囲
    [SerializeField, Header("見える範囲")]
    private float m_viewAngle = 45.0f;

    [SerializeField, Header("視界の距離（半径）")]
    private float m_viewDistance = 5.5f;

    [SerializeField, Header("プレイヤーのレイヤー")]
    private LayerMask m_playerLayer;

    [SerializeField, Header("壁のレイヤー")]
    private LayerMask m_wallLayer;

    // エネミーコンポーネント
    private EnemyManager m_enemyComp;

    // プレイヤーが見えているか判定するフラグ
    private bool m_isPlayerVisible = false;

    void Awake()
    {
        // コンポーネントを取得
        m_enemyComp = GetComponent<EnemyManager>();
    }

    // 視界内にいるプレイヤーを検出する関数
    public void DetectPlayerInView()
    {
        // プレイヤーの位置を中心に球体で敵候補を探す
        Collider[] hits = Physics.OverlapSphere(transform.position, m_viewDistance, m_playerLayer);

        foreach (var hit in hits)
        {
            Transform playerTransform = hit.transform;
            // 敵までの方向ベクトルを取得
            Vector3 dirToEnemy = (playerTransform.position - transform.position).normalized;
            // 視線方向との角度を取得する
            float angleToEnemy = Vector3.Angle(transform.forward, dirToEnemy);

            // m_viewAngleに収まっているかどうか
            if (angleToEnemy < m_viewAngle)
            {
                // プレイヤーと敵の距離を求める
                float distanceToEnemy = Vector3.Distance(transform.position, playerTransform.position);

                // 障害物が無いかレイで判定
                if (!Physics.Raycast(transform.position, dirToEnemy, distanceToEnemy, m_wallLayer))
                {
                    if(!m_isPlayerVisible)
                    {
                        // フラグを上げる
                        m_isPlayerVisible = true;
                        AudioManager.Instance.RegisterEnemyVision();
                    }
                    m_enemyComp.SetChasePlayer(true);
                    return;
                } 
            }
        }
 
        if (m_isPlayerVisible)
        {
            // フラグを下げる
            m_isPlayerVisible = false;
            AudioManager.Instance.UnregisterEnemyVision();
        }

        // 見えていなかったらフラグを下げる
        m_enemyComp.SetChasePlayer(false);
    }

    // デバッグ用：視界範囲をSceneビューで表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0, -m_viewAngle, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, m_viewAngle, 0) * transform.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, leftBoundary * m_viewDistance);
        Gizmos.DrawRay(transform.position, rightBoundary * m_viewDistance);
    }
}