using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy_Search : MonoBehaviour
{
    // ���g�̃X�^�[�g�ʒu���i�[����ϐ�
    private Vector3 m_startPos;

    // �v���C���[���i�[����ϐ�
    private GameObject m_player;

    // �z��̃C���f�b�N�X�ԍ��w��p�ϐ�
    private int m_currentIndex = 0;

    // �ړI�n��ݒ肷��ϐ�
    [SerializeField, Header("�ړI�n")]
    private Transform[] m_patrolPoints = null;

    // �i�r�Q�[�V�����̃R���|�[�l���g���i�[����ϐ�
    private NavMeshAgent m_agent;

    void Awake()
    {
        // �v���C���[���擾
        m_player = GameObject.FindWithTag("Player");
        // �X�^�[�g�ʒu��ۑ�����
        m_startPos = transform.position;
        // �R���|�[�l���g�擾
        m_agent = GetComponent<NavMeshAgent>();
    }

    public void StartPatrol()
    {
        // �G�����̖ړI�n�Ɍ������ē�����
        m_agent.destination = m_patrolPoints[m_currentIndex].position;
    }

    // ���̖ړI�n�Ɉړ�����֐�
    void UpdatePatrolPoint()
    {
        // �C���f�b�N�X�ԍ����X�V����i�ŏI�ԍ��ɂȂ�����ŏ��̔ԍ��ɖ߂��j
        m_currentIndex = (m_currentIndex + 1) % m_patrolPoints.Length;

        // �G�����̖ړI�n�Ɍ������ē�����
        m_agent.destination = m_patrolPoints[m_currentIndex].position;
    }

    // �G�̈ړ��p�֐�
    public void UpdateMove(bool isChase)
    {
        // �v���C���[��ǂ��t���O�������Ă�����
        if (isChase)
        {
            // �ړ����x���グ��
            m_agent.speed = 3.0f;
            // �v���C���[�̈ʒu�Ɍ������Ĉړ�����
            m_agent.SetDestination(m_player.transform.position);
        }
        else
        {
            // �ړ����x��߂�
            m_agent.speed = 2.0f;
            // m_agent.remainingDistance�͓G�Ǝ��̖ړI�n�܂ł̋�����\���Ă���
            // �߂Â��ق�0�ɋ߂Â��Ă���
            if (m_agent.remainingDistance < 0.5f)
            {
                // ���̖ړI�n�Ɍ�����
                UpdatePatrolPoint();
            }
        }
    }

    // �G�̃X�^���̏������܂Ƃ߂��֐�
    public void HandleStan(bool isStan)
    {
        if (isStan)
        {
            // �ړ����x��0�ɂ���
            m_agent.speed = 0.0f;
        }
        else
        {
            // �ړ����x��߂�
            m_agent.speed = 2.0f;
        }
    }

    // �v���C���[�ɓ��������ۂɏ���������֐�
    public void ResetPosition()
    {
        // �����ʒu�Ƀ��Z�b�g
        transform.position = m_startPos;

        // �G���ŏ��̖ړI�n�Ɍ������ē�����
        m_currentIndex = 0;
        m_agent.destination = m_patrolPoints[m_currentIndex].position;

        // �ړ����x��߂�
        m_agent.speed = 2.0f;
    }
}
