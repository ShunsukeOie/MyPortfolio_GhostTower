using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //�v���C���[�̈ړ����x
    [SerializeField]
    [Header("�ړ����x")] public float MoveSpeed = 0.15f;

    //�v���C���[�̉�]�X�s�[�h
    [SerializeField]
    [Header("��]�X�s�[�h")] public float RotationSpeed = 0.15f;

    // Update is called once per frame
    void Update()
    {
        //���X�e�B�b�N�ňړ�
        PlayerMove();

        //�E�X�e�B�b�N�ŉ�]
        PlayerRotate();
      
    }

    //�v���C���[�̈ړ��p���\�b�h
    void PlayerMove()
    {
        //���X�e�B�b�N�̐������͎��̒l���擾����(�E�F�v���X�A���F�}�C�i�X)
        float horizontakInput = Input.GetAxis("Horizontal");

        //���X�e�B�b�N�̐������͎��̒l���擾����i��F�v���X�A���F�}�C�i�X�j
        float verticalInput = Input.GetAxis("Vertical");

        //���X�e�B�b�N�̓��͂����`���ꂽ���W�Ɉړ�
        transform.localPosition += new Vector3(horizontakInput, 0, verticalInput) * MoveSpeed * Time.deltaTime;
    }

    //�v���C���[�̉�]�p���\�b�h
    void PlayerRotate()
    {
        //�E�X�e�B�b�N�̐������͎��̒l���擾����
        float horizontak2Input = Input.GetAxis("Horizontal2");

        //�E�X�e�B�b�N�Ńv���C���[����]
        transform.Rotate(new Vector3(0, RotationSpeed * horizontak2Input, 0));
    }
}
