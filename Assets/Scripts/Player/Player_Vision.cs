using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Vision : MonoBehaviour
{
    // �t���b�V���͈̔�
    [SerializeField, Header("�t���b�V���͈̔�")]
    private float m_viewAngle = 45.0f;

    [SerializeField, Header("���E�̋����i���a�j")]
    private float m_viewDistance = 5.5f;

    [SerializeField, Header("�G�l�~�[�̃��C���[")]
    private LayerMask m_enemyLayer;

    [SerializeField, Header("�ǂ̃��C���[")]
    private LayerMask m_wallLayer;

    // �G�l�~�[�̃X�N���v�g�擾�p�ϐ�
    private List<Enemy> m_enemyList = new List<Enemy>();

    // ���E���ɂ���G�����o����֐�
    public void DetectEemiesInView()
    {
        m_enemyList.Clear();
        // �v���C���[�̈ʒu�𒆐S�ɋ��̂œG����T��
        Collider[] hits = Physics.OverlapSphere(transform.position, m_viewDistance, m_enemyLayer);

        foreach(var hit in hits)
        {
            Transform enemyTransform = hit.transform;
            // �G�܂ł̕����x�N�g�����擾
            Vector3 dirToEnemy = (enemyTransform.position - transform.position).normalized;
            // ���������Ƃ̊p�x���擾����
            float angleToEnemy = Vector3.Angle(transform.forward, dirToEnemy);

            // m_viewAngle�Ɏ��܂��Ă��邩�ǂ���
            if (angleToEnemy < m_viewAngle) 
            {
                // �v���C���[�ƓG�̋��������߂�
                float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

                // ��Q�������������C�Ŕ���
                if (!Physics.Raycast(transform.position, dirToEnemy, distanceToEnemy, m_wallLayer))
                {
                    // �G�̃X�N���v�g�擾
                    Enemy enemy = enemyTransform.GetComponent<Enemy>();
                    // �܂����X�g�Ɋ܂܂�Ă��Ȃ��Ȃ�Ώ����i�d�����Ȃ��悤�Ɂj
                    if (enemy != null)
                    {
                        // ���X�g�ɒǉ�����
                        m_enemyList.Add(enemy);
                    }
                }
            }
        }
    }

    // �G�l�~�[�̃��X�g��Ԃ��֐�
    public List<Enemy> GetEnemies()
    {
        return m_enemyList;
    }

    // �f�o�b�O�p�F���E�͈͂�Scene�r���[�ŕ\��
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
