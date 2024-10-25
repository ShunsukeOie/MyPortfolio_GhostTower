using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Camera : MonoBehaviour 
{

    //カメラを格納
    public Camera main_camera;
    public Camera sub_camera;

    // Start is called before the first frame update
    void Start()
    {
        //一人称カメラを最初はオフにする
        sub_camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Cボタンを押したときの処理
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(!sub_camera.enabled)
            {
                sub_camera.enabled = true;

                main_camera.enabled = false;
            }
            else
            {
                sub_camera.enabled = false;

                main_camera.enabled = true;
            }
        }
    }
}
