using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //�v���C���[�̈ړ����x
    [SerializeField]
    [Header("�ړ����x")] public float m_MoveSpeed = 0.15f;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //�������͎��̒l���擾����
        float horizontakInput = Input.GetAxis("Horizontal");

        //�������͎��̒l���擾����
        float verticalInput = Input.GetAxis("Vertical");

        //���͂��ꂽ��`���ꂽ���W�Ɉړ�
        transform.Translate(new Vector3(horizontakInput,0,verticalInput) * m_MoveSpeed * Time.deltaTime)
    }
}
