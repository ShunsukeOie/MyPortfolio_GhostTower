using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Vision : MonoBehaviour
{
    // フラッシュの範囲
    [SerializeField, Header("フラッシュの範囲")]
    private float m_viewAngle = 45.0f;

    [SerializeField, Header("視界の距離（半径）")]
    private float m_viewDistance = 5.5f;

    [SerializeField, Header("エネミーのレイヤー")]
    private LayerMask m_enemyLayer;

    [SerializeField, Header("壁のレイヤー")]
    private LayerMask m_wallLayer;

    // エネミーのスクリプト取得用変数
    private List<Enemy> m_enemyList = new List<Enemy>();

    // 視界内にいる敵を検出する関数
    public void DetectEemiesInView()
    {
        m_enemyList.Clear();
        // プレイヤーの位置を中心に球体で敵候補を探す
        Collider[] hits = Physics.OverlapSphere(transform.position, m_viewDistance, m_enemyLayer);

        foreach(var hit in hits)
        {
            Transform enemyTransform = hit.transform;
            // 敵までの方向ベクトルを取得
            Vector3 dirToEnemy = (enemyTransform.position - transform.position).normalized;
            // 視線方向との角度を取得する
            float angleToEnemy = Vector3.Angle(transform.forward, dirToEnemy);

            // m_viewAngleに収まっているかどうか
            if (angleToEnemy < m_viewAngle) 
            {
                // プレイヤーと敵の距離を求める
                float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

                // 障害物が無いかレイで判定
                if (!Physics.Raycast(transform.position, dirToEnemy, distanceToEnemy, m_wallLayer))
                {
                    // 敵のスクリプト取得
                    Enemy enemy = enemyTransform.GetComponent<Enemy>();
                    // まだリストに含まれていないならば処理（重複しないように）
                    if (enemy != null)
                    {
                        // リストに追加する
                        m_enemyList.Add(enemy);
                    }
                }
            }
        }
    }

    // エネミーのリストを返す関数
    public List<Enemy> GetEnemies()
    {
        return m_enemyList;
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
