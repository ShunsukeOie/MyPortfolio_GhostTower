using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //プレイヤーの移動速度
    [SerializeField]
    [Header("移動速度")] public float m_MoveSpeed = 0.15f;

    //プレイヤーの回転スピード
    [SerializeField]
    [Header("回転スピード")] public float m_RotationSpeed = 0.15f;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //左スティックの水平入力軸の値を取得する
        float horizontakInput = Input.GetAxis("Horizontal");

        //左スティックの垂直入力軸の値を取得する
        float verticalInput = Input.GetAxis("Vertical");

        //左スティックの入力から定義された座標に移動
        transform.localPosition += new Vector3(horizontakInput, 0, verticalInput) * m_MoveSpeed * Time.deltaTime;

        //右スティックの水平入力軸の値を取得する
        float horizontak2Input = Input.GetAxis("Horizontal2");

        //右スティックでプレイヤーを回転
        transform.Rotate(new Vector3(0, m_RotationSpeed * horizontak2Input,0));
    }
}
