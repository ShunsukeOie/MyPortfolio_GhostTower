using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy_Search : MonoBehaviour
{
    // ���g�̃X�^�[�g�ʒu���i�[����ϐ�
    private Vector3 m_startPos;

    // �ړI�n��ݒ肷��ϐ�
    [SerializeField, Header("�ړI�n")]
    private Transform[] m_goals = null;
    // �i�r�Q�[�V�����̃R���|�[�l���g���i�[����ϐ�
    private NavMeshAgent m_agent;
    // �z��̃C���f�b�N�X�ԍ��w��p�ϐ�
    private int m_destNum = 0;


    Player_Light m_plyLight;
    // �v���C���[�̈ʒu���i�[����ϐ�
    [SerializeField]
    private Transform m_player;

    // ������͈�
    [SerializeField, Header("������͈�")]
    private float m_angle = 45.0f;

    public bool isStan = false;
    private float m_stanTime = 1.0f;
    private float m_stanTimer = 0.0f;

    void Start()
    {
        // �X�^�[�g�ʒu��ۑ�����
        m_startPos = transform.position;
        // �R���|�[�l���g�擾
        m_agent = GetComponent<NavMeshAgent>();
        m_plyLight = GetComponent<Player_Light>();
        // �G�����̖ړI�n�Ɍ������ē�����
        m_agent.destination = m_goals[m_destNum].position;
    }

    void Update()
    {
        if(isStan)
        {
            m_agent.speed = 0.0f;
            m_stanTimer += Time.deltaTime;
            if(m_stanTimer >= m_stanTime)
            {
                m_agent.speed = 2.0f;
                m_stanTimer = 0.0f;
                isStan = false;
            }
        }
        else
        {
            // m_agent.remainingDistance�͓G�Ǝ��̖ړI�n�܂ł̋�����\���Ă���
            // �߂Â��ق�0�ɋ߂Â��Ă���
            if (m_agent.remainingDistance < 0.5f)
            {
                // �v���C���[�������ĂȂ��Ƃ��̑��x
                m_agent.speed = 2.0f;
                nextGoal();
            }
        }
    }

    // ���̖ړI�n�Ɉړ�����֐�
    void nextGoal()
    {
        // �C���f�b�N�X�ԍ����X�V����
        m_destNum += 1;
        // �ŏI�ԍ��ɂȂ�����
        if (m_destNum == 3)
        {
            // �ŏ��̔ԍ��ɖ߂�
            m_destNum = 0;
        }
        // �G�����̖ړI�n�Ɍ������ē�����
        m_agent.destination = m_goals[m_destNum].position;

        Debug.Log(m_destNum);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ɐG�ꂽ�珈������
        if(collision.gameObject.tag == "Player")
        {
            // ���̈ʒu�ɖ߂�
            transform.position = m_startPos;
        }
    }


    // �͈͓��ɓ����Ă�����
    private void OnTriggerStay(Collider other)
    {
        // �^�O���v���C���[�����ʂ���
        if(other.gameObject.tag == "Player")
        {
            // ���ʂɑ΂��āA�v���C���[�̈ʒu���擾���A45�x�ȓ����Z�o
            Vector3 posDelta = other.transform.position - this.transform.position;
            // Angle()�֐��Ő��ʂɑ΂��ĉ��x�̊p�x�����擾����
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            // target_angle��m_angle�Ɏ��܂��Ă��邩�ǂ���
            if(target_angle < m_angle)
            {
                // ���C���g�p����targe�ɓ������Ă��邩���ʂ���
                if(Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // ���C�ɓ��������̂��v���C���[�������珈������
                    if(hit.collider == other)
                    {
                        // �v���C���[���������瑁���Ȃ�
                        m_agent.speed = 3.0f;
                        // �v���C���[�̈ʒu�Ɍ������Ĉړ�����
                        m_agent.SetDestination(m_player.position);
                        Debug.Log("�����Ă���");
                    }
                }
            }
        }
    }
}
