using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    // �X�^�[�g�ʒu�i�[�p
    private Vector3 startPos;

    [SerializeField, Header("AudioManger�I�u�W�F�N�g")]
    private GameObject _audiomng;
    // AudioManager�̃X�N���v�g�i�[�p
    private AudioManager _audioscript;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g���擾
        _audioscript = _audiomng.GetComponent<AudioManager>();

        // �X�^�[�g�ʒu���i�[����
        startPos = transform.position;
    }



    private void OnCollisionEnter(Collision collision)
    {
        // �^�O��Enemy�̃I�u�W�F�N�g�ɓ��������珈������
        if (collision.gameObject.tag == "Enemy")
        {
            // �X�^�[�g�ʒu�ɖ߂�
            transform.position = startPos;

            // ���ꉹ�𗬂�
            _audioscript.PlayerDeadSE();
        }

        // �^�O��Item�̃I�u�W�F�N�g�ɓ��������珈������
        if(collision.gameObject.tag == "Item")
        {
            // �A�C�e���Q�b�g�̉��𗬂�
            _audioscript.ItemGetSE();
        }
    }
}
