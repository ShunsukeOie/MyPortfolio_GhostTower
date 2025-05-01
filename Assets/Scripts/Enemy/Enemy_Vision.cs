using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision : MonoBehaviour
{
    // �G�l�~�[�̃I�u�W�F�N�g
    [SerializeField]
    private GameObject m_enemyObj;
    // �G�l�~�[�̃I�u�W�F�N�g�̃X�N���v�g�擾�p
    private Enemy m_enemyComp;

    // ������͈�
    [SerializeField, Header("������͈�")]
    private float m_viewAngle = 45.0f;


    // Start is called before the first frame update
    void Start()
    {
        // �G�l�~�[���擾
        m_enemyObj = transform.parent.gameObject;
        // �X�N���v�g�擾
        m_enemyComp = m_enemyObj.GetComponent<Enemy>();
    }

    // �͈͓��ɓ����Ă�����
    private void OnTriggerStay(Collider other)
    {
        // �G�l�~�[���X�^����Ԃł͂Ȃ����^�O��Player�̂Ƃ�
        if (other.gameObject.tag == "Player" && !m_enemyComp.m_isStan)
        {
            // ���ʂɑ΂��āA�v���C���[�̈ʒu���擾���A45�x�ȓ����Z�o
            Vector3 posDelta = other.transform.position - this.transform.position;
            // Angle()�֐��Ő��ʂɑ΂��ĉ��x�̊p�x�����擾����
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            // target_angle��m_angle�Ɏ��܂��Ă��邩�ǂ���
            if (target_angle < m_viewAngle)
            {
                // ���C���g�p����Player�ɓ������Ă��邩���ʂ���
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // ���C�ɓ��������̂��v���C���[�������珈������
                    if (hit.collider == other)
                    {
                        m_enemyComp.SetChasePlayer(true);
                    }
                }
            }
            // �G���v���C���[��ǂ�Ȃ�����
            else
            {
                // �t���O���~�낷
                m_enemyComp.SetChasePlayer(false);
            }
        }
    }
}