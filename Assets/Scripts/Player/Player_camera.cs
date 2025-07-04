using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_camera : MonoBehaviour
{
    //プレイヤーの移動速度
    [SerializeField]
    [Header("移動速度")] public float MoveSpeed = 0.15f;

    //プレイヤーの回転スピード
    [SerializeField]
    [Header("回転スピード")] public float RotationSpeed = 0.15f;


    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //左スティックで移動
        PlayerMove();

        //右スティックで回転
        PlayerRotate();

    }

    //プレイヤーの移動用メソッド
    void PlayerMove()
    {
        //左スティックの水平入力軸の値を取得する
        float horizontakInput = Input.GetAxis("Horizontal")　* MoveSpeed;

        //左スティックの垂直入力軸の値を取得する
        float verticalInput = Input.GetAxis("Vertical") * MoveSpeed;

        //左スティックの入力から定義された座標に移動
        transform.position += cam.transform.forward * verticalInput + cam.transform.right * horizontakInput;
    }

    //プレイヤーの回転用メソッド
    void PlayerRotate()
    {

        //右スティックの水平入力軸の値を取得する
        float horizontak2Input = Input.GetAxis("Horizontal2");

        //右スティックでプレイヤーを回転
        transform.Rotate(new Vector3(0, RotationSpeed * horizontak2Input, 0));


    }
}
