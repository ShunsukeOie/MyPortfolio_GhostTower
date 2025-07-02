using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    // �ړ��̏������܂Ƃ߂��X�N���v�g
    private Player_Move m_moveComp;

    // ���C�g�̏������܂Ƃ߂��X�N���v�g
    private Player_Light m_lightComp;

    // �����^���𓔂��������܂Ƃ߂��X�N���v�g
    private Player_Lantern m_lanternComp;

    private Player_Vision m_visionComp;

    // �X�^�[�g�ʒu�i�[�p
    private Vector3 m_startPos;

    void Start()
    {
        // �R���|�[�l���g���擾
        m_moveComp = GetComponent<Player_Move>();
        m_lightComp = GetComponent<Player_Light>();
        m_lanternComp = GetComponent<Player_Lantern>();
        m_visionComp = GetComponent<Player_Vision>();

        // �X�^�[�g�ʒu���i�[����
        m_startPos = transform.position;
    }

    void Update()
    {
        // �ړ��A��]����
        m_moveComp.PlayerMove();
        m_moveComp.PlayerRotate();

        // ���C�g�̍X�V����
        // �o�b�e���[�̎c�ʂ��X�V
        m_lightComp.UpdateBattery();

        // ���C�g��_������
        m_lightComp.LightUp();

        // �t���b�V���g�p���̏���
        m_lightComp.Flash();

        // �o�b�e���[�̎c�ʂŃQ�[�W�̐F��ύX����
        m_lightComp.UpdateGaugeColor();

        //�ڂ̑O�Ƀ����^�������邩���`�F�b�N
        m_lanternComp.LanternCheck();

        // �G�����E���ɂ��邩�`�F�b�N
        m_visionComp.DetectEemiesInView();

        // �����^���̌��𓔂�
        m_lanternComp.LightUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            // �^�O��Enemy�̏ꍇ
            case "Enemy":
                // �X�^�[�g�ʒu�ɖ߂�
                transform.position = m_startPos;

                // ���ꉹ�𗬂�
                AudioManager.Instance.PlayerDeadSE();
                break;

            // �^�O��Item�̏ꍇ
            case "Item":
                // �A�C�e���Q�b�g�̉��𗬂�
                AudioManager.Instance.ItemGetSE();

                // �o�b�e���[���񕜂���
                m_lightComp.HealBattery();

                // �A�C�e�����폜
                Destroy(other.gameObject);
                break;
        }
    }
}
