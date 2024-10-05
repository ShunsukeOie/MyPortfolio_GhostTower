using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //�v���C���[�̈ړ����x
    [SerializeField]
    [Header("�ړ����x")] public float m_MoveSpeed = 0.15f;

    //�v���C���[�̉�]�X�s�[�h
    [SerializeField]
    [Header("��]�X�s�[�h")] public float m_RotationSpeed = 0.15f;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //���X�e�B�b�N�̐������͎��̒l���擾����
        float horizontakInput = Input.GetAxis("Horizontal");

        //���X�e�B�b�N�̐������͎��̒l���擾����
        float verticalInput = Input.GetAxis("Vertical");

        //���X�e�B�b�N�̓��͂����`���ꂽ���W�Ɉړ�
        transform.localPosition += new Vector3(horizontakInput, 0, verticalInput) * m_MoveSpeed * Time.deltaTime;

        //�E�X�e�B�b�N�̐������͎��̒l���擾����
        float horizontak2Input = Input.GetAxis("Horizontal2");

        //�E�X�e�B�b�N�Ńv���C���[����]
        transform.Rotate(new Vector3(0, m_RotationSpeed * horizontak2Input,0));
    }
}
