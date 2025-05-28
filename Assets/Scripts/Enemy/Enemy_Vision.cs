using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision : MonoBehaviour
{
    // ������͈�
    [SerializeField, Header("������͈�")]
    private float m_viewAngle = 45.0f;

    [SerializeField, Header("���E�̋����i���a�j")]
    private float m_viewDistance = 5.5f;

    [SerializeField, Header("�v���C���[�̃��C���[")]
    private LayerMask m_playerLayer;

    [SerializeField, Header("�ǂ̃��C���[")]
    private LayerMask m_wallLayer;

    // �G�l�~�[�R���|�[�l���g
    private EnemyManager m_enemyComp;

    // �v���C���[�������Ă��邩���肷��t���O
    private bool m_isPlayerVisible = false;

    void Awake()
    {
        // �R���|�[�l���g���擾
        m_enemyComp = GetComponent<EnemyManager>();
    }

    // ���E���ɂ���v���C���[�����o����֐�
    public void DetectPlayerInView()
    {
        // �v���C���[�̈ʒu�𒆐S�ɋ��̂œG����T��
        Collider[] hits = Physics.OverlapSphere(transform.position, m_viewDistance, m_playerLayer);

        foreach (var hit in hits)
        {
            Transform playerTransform = hit.transform;
            // �G�܂ł̕����x�N�g�����擾
            Vector3 dirToEnemy = (playerTransform.position - transform.position).normalized;
            // ���������Ƃ̊p�x���擾����
            float angleToEnemy = Vector3.Angle(transform.forward, dirToEnemy);

            // m_viewAngle�Ɏ��܂��Ă��邩�ǂ���
            if (angleToEnemy < m_viewAngle)
            {
                // �v���C���[�ƓG�̋��������߂�
                float distanceToEnemy = Vector3.Distance(transform.position, playerTransform.position);

                // ��Q�������������C�Ŕ���
                if (!Physics.Raycast(transform.position, dirToEnemy, distanceToEnemy, m_wallLayer))
                {
                    if(!m_isPlayerVisible)
                    {
                        // �t���O���グ��
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
            // �t���O��������
            m_isPlayerVisible = false;
            AudioManager.Instance.UnregisterEnemyVision();
        }

        // �����Ă��Ȃ�������t���O��������
        m_enemyComp.SetChasePlayer(false);
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