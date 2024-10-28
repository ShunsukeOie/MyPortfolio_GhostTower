using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Judge : MonoBehaviour
{
    // ������͈�
    [SerializeField, Header("������͈�")]
    private float angle = 45.0f;

    // �G�l�~�[�̃I�u�W�F�N�g
    [SerializeField]
    private GameObject EnemyObj;
    // �G�l�~�[�̃I�u�W�F�N�g�̃X�N���v�g�擾�p
    private Enemy_Search _search;

    // Start is called before the first frame update
    void Start()
    {
        // �X�N���v�g�擾
        _search = EnemyObj.GetComponent<Enemy_Search>();
    }

    // �͈͓��ɓ����Ă�����
    private void OnTriggerStay(Collider other)
    {
        // �G�l�~�[���X�^����Ԃł͂Ȃ��Ƃ�
        if (_search.isStan == false)
        {
            // �^�O���v���C���[�����ʂ���
            if (other.gameObject.tag == "Player")
            {
                // ���ʂɑ΂��āA�v���C���[�̈ʒu���擾���A45�x�ȓ����Z�o
                Vector3 posDelta = other.transform.position - this.transform.position;
                // Angle()�֐��Ő��ʂɑ΂��ĉ��x�̊p�x�����擾����
                float target_angle = Vector3.Angle(this.transform.forward, posDelta);

                // target_angle��m_angle�Ɏ��܂��Ă��邩�ǂ���
                if (target_angle < angle)
                {
                    // ���C���g�p����Player�ɓ������Ă��邩���ʂ���
                    if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                    {
                        // ���C�ɓ��������̂��v���C���[�������珈������
                        if (hit.collider == other)
                        {
                            // �v���C���[���������瑁���Ȃ�
                            _search._agent.speed = 3.0f;
                            _search.isChasePlayer = true;

                            // �G���ǂ��Ă���Ƃ��p�̉��ɐ؂�ւ���ׁAAudioManager�̃t���O��ς���
                            AudioManager mng = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                            mng.ChangeAudio = true;
                        }
                    }
                }
                // �G���v���C���[��ǂ�Ȃ�����
                else
                {
                    // �t���O���~�낷
                    _search.isChasePlayer = false;
                    // �G���v���C���[��ǂ��O�̖ړI�n�Ɍ������ē�����
                    _search._agent.destination = _search.goals[_search.destNum].position;

                    // �f�t�H���g�̉��ɐ؂�ւ���ׁAAudioManager�̃t���O��ς���
                    AudioManager mng = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                    mng.ChangeAudio = false;
                }
            }
        }
    }
}
