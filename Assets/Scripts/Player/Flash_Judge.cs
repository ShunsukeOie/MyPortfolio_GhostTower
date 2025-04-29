using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Judge : MonoBehaviour
{
    // �t���b�V���͈̔�
    [SerializeField, Header("�t���b�V���͈̔�")]
    private float m_viewAngle = 45.0f;

    // �G�l�~�[�̃X�N���v�g�擾�p�ϐ�
    private List<Enemy> m_enemyList = new List<Enemy>();

    // �͈͓��ɓ����Ă�����
    private void OnTriggerStay(Collider other)
    {
        // �^�O���G�l�~�[�����ʂ���
        if (other.gameObject.tag == "Enemy")
        {
            // ���ʂɑ΂��āA�v���C���[�̈ʒu���擾���A45�x�ȓ����Z�o
            Vector3 posDelta = other.transform.position - this.transform.position;
            // Angle()�֐��Ő��ʂɑ΂��ĉ��x�̊p�x�����擾����
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            // target_angle��m_angle�Ɏ��܂��Ă��邩�ǂ���
            if (target_angle < m_viewAngle)
            {
                // ���C���g�p����Enemy�ɓ������Ă��邩���ʂ���
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // ���C�ɓ��������̂��G�l�~�[�������珈������
                    if (hit.collider == other)
                    {
                        // �G�̃X�N���v�g�擾
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
            // �G�̃X�N���v�g�擾
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
