using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Camera : MonoBehaviour 
{

    //�J�������i�[
    public Camera main_camera;
    public Camera sub_camera;

    //�v���C���[�I�u�W�F�N�g���擾
    [SerializeField] GameObject player_move;

    // Start is called before the first frame update
    void Start()
    {
        //��l�̃J�������ŏ��̓I�t�ɂ���
        sub_camera.enabled = false;

        player_move = GameObject.Find("Player");

        //��������̐ݒ�
        player_move.GetComponent<Player_Move>().enabled = true;
        player_move.GetComponent<Player_camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //C�{�^�����������Ƃ��̏���
        if (Input.GetKeyDown(KeyCode.C))
        {
            //��l�̃��[�h�ɂ���
            if(!sub_camera.enabled)
            {
                sub_camera.enabled = true;

                main_camera.enabled = false;

                player_move.GetComponent<Player_Move>().enabled = false;
                player_move.GetComponent<Player_camera>().enabled = true;
            }
            //�����낵�^�ɂ���
            else
            {
                sub_camera.enabled = false;

                main_camera.enabled = true;

                player_move.GetComponent<Player_Move>().enabled = true;
                player_move.GetComponent<Player_camera>().enabled = false;
            }
        }
    }
}
