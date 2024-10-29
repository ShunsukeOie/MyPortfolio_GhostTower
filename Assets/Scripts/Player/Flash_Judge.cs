using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Judge : MonoBehaviour
{
    // �t���b�V���͈̔�
    [SerializeField, Header("�t���b�V���͈̔�")]
    private float angle = 45.0f;

    // �G�l�~�[�̃X�N���v�g�擾�p�ϐ�
    [HideInInspector]
    public Enemy_Search _esearch = null;

    // �v���C���[�̃I�u�W�F�N�g
    [SerializeField, Header("�v���C���[�I�u�W�F")]
    private GameObject player;
    // �v���C���[�̃X�N���v�g�i�[�p
    private Player_Light _plyLight;

    private void Start()
    {
        // �X�N���v�g�擾
        _plyLight = player.GetComponent<Player_Light>();
    }

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
            if (target_angle < angle)
            {
                // ���C���g�p����Enemy�ɓ������Ă��邩���ʂ���
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // ���C�ɓ��������̂��G�l�~�[�������珈������
                    if (hit.collider == other)
                    {
                        // �G�̃X�N���v�g�擾
                        _esearch = other.GetComponent<Enemy_Search>();
                        // �v���C���[�̃X�N���v�g�ɃA�N�Z�X���A�t���O���グ��
                        _plyLight.canStopEnemy = true;
                    }
                }
                Debug.DrawRay(transform.position, posDelta, Color.red);
            }
            else
            {
                // �X�N���v�g�i�[�p�ϐ���null�ɂ��Ă���
                _esearch = null;
                // �p�x���ɂ��Ȃ�������t���O���~�낷
                _plyLight.canStopEnemy = false;
            }
        }
    }
}
