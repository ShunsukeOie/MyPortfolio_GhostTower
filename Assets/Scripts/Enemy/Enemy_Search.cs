using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy_Search : MonoBehaviour
{
    // ���g�̃X�^�[�g�ʒu���i�[����ϐ�
    private Vector3 startPos;

    // �v���C���[�̈ʒu���i�[����ϐ�
    [SerializeField]
    private Transform player;

    // ������͈�
    [SerializeField, Header("������͈�")]
    private float angle = 45.0f;

    // �X�^�����Ԍv���p
    private float stanTime = 2.0f;
    private float stanTimer = 0.0f;

    // �ړI�n��ݒ肷��ϐ�
    [Header("�ړI�n")]
    public Transform[] goals = null;

    // �i�r�Q�[�V�����̃R���|�[�l���g���i�[����ϐ�
    [HideInInspector]
    public NavMeshAgent _agent;

    // �z��̃C���f�b�N�X�ԍ��w��p�ϐ�
    [HideInInspector]
    public int destNum = 0;

    // �v���C���[��ǂ��Ă��邩�𔻒肷��ϐ�
    [HideInInspector]
    public bool isChasePlayer = false;

    // �X�^����Ԃ��ǂ����̃t���O
    [HideInInspector]
    public bool isStan = false;

    

    void Start()
    {
        // �X�^�[�g�ʒu��ۑ�����
        startPos = transform.position;
        // �R���|�[�l���g�擾
        _agent = GetComponent<NavMeshAgent>();
        // �G�����̖ړI�n�Ɍ������ē�����
        _agent.destination = goals[destNum].position;
    }

    void Update()
    {
        Move();
    }

    // �G�̈ړ��p�֐�
    void Move()
    {
        // �X�^����Ԃ�������i�v���C���[���烉�C�g��H�������true�ɂȂ�j
        if (isStan)
        {
            // �ړ����x��0�ɂ���
            _agent.speed = 0.0f;

            // ���Ԃ����Z����
            stanTimer += Time.deltaTime;
            // �^�C�}�[���X�^�����Ԃ𒴂�����
            if (stanTimer >= stanTime)
            {
                // �X�s�[�h�����ɖ߂�
                _agent.speed = 2.0f;
                // ���Ԃ����Z�b�g����
                stanTimer = 0.0f;
                // �t���O���~�낷
                isStan = false;
            }
        }
        // �X�^����Ԃ���Ȃ�
        else
        {
            // m_agent.remainingDistance�͓G�Ǝ��̖ړI�n�܂ł̋�����\���Ă���
            // �߂Â��ق�0�ɋ߂Â��Ă���
            if (_agent.remainingDistance < 0.5f && !isChasePlayer)
            {
                // �v���C���[�������ĂȂ��Ƃ��̑��x
                _agent.speed = 2.0f;
                // ���̖ړI�n�Ɍ�����
                nextGoal();
            }

            // �v���C���[��ǂ��t���O�������Ă�����
            if (isChasePlayer)
            {
                // �v���C���[�̈ʒu�Ɍ������Ĉړ�����
                _agent.SetDestination(player.position);
            }
        }
    }

    // ���̖ړI�n�Ɉړ�����֐�
    void nextGoal()
    {
        // �C���f�b�N�X�ԍ����X�V����
        destNum += 1;
        // �ŏI�ԍ��ɂȂ�����
        if (destNum == 8)
        {
            // �ŏ��̔ԍ��ɖ߂�
            destNum = 0;
        }
        // �G�����̖ړI�n�Ɍ������ē�����
        _agent.destination = goals[destNum].position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ɐG�ꂽ�珈������
        if(collision.gameObject.tag == "Player")
        {
            // ���̈ʒu�ɖ߂�
            transform.position = startPos;

            // �G���ŏ��̖ړI�n�Ɍ������ē�����
            destNum = 0;
            _agent.destination = goals[destNum].position;

            // �v���C���[��ǂ�Ȃ�����
            isChasePlayer = false;

            // ���x�����ɖ߂�
            _agent.speed = 2.0f;
        }
    }
}
