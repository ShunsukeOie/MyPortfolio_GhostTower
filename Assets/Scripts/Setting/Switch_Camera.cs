using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Camera : MonoBehaviour 
{

    //カメラを格納
    public Camera main_camera;
    public Camera sub_camera;

    //プレイヤーオブジェクトを取得
    [SerializeField] GameObject player_move;

    // Start is called before the first frame update
    void Start()
    {
        //一人称カメラを最初はオフにする
        sub_camera.enabled = false;

        player_move = GameObject.Find("Player");

        //初期操作の設定
        player_move.GetComponent<Player_Move>().enabled = true;
        player_move.GetComponent<Player_camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Cボタンを押したときの処理
        if (Input.GetKeyDown(KeyCode.C))
        {
            //一人称モードにする
            if(!sub_camera.enabled)
            {
                sub_camera.enabled = true;

                main_camera.enabled = false;

                player_move.GetComponent<Player_Move>().enabled = false;
                player_move.GetComponent<Player_camera>().enabled = true;
            }
            //見下ろし型にする
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
