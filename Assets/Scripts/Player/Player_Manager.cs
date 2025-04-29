using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    //���C�g�̏������A�^�b�`
    private Player_Light m_lightScript;


    // �X�^�[�g�ʒu�i�[�p
    private Vector3 m_startPos;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g���擾
        m_lightScript = GetComponent<Player_Light>();

        // �X�^�[�g�ʒu���i�[����
        m_startPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �^�O��Enemy�̃I�u�W�F�N�g�ɓ��������珈������
        if (other.gameObject.tag == "Enemy")
        {
            // �X�^�[�g�ʒu�ɖ߂�
            transform.position = m_startPos;

            // ���ꉹ�𗬂�
            AudioManager.instance.PlayerDeadSE();
        }

        // �^�O��Item�̃I�u�W�F�N�g�ɓ��������珈������
        if (other.gameObject.tag == "Item")
        {
            // �A�C�e���Q�b�g�̉��𗬂�
            AudioManager.instance.ItemGetSE();

            // �o�b�e���[���񕜂���
            m_lightScript.HealBattery();

            // �A�C�e�����폜
            Destroy(other.gameObject);
        }
    }
}
