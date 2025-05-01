using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //プレイヤーの移動速度
    [SerializeField]
    [Header("移動速度")] private float m_moveSpeed = 3.0f;

    //プレイヤーの回転スピード
    [SerializeField]
    [Header("回転スピード")] private float m_rotationSpeed = 4.0f;

    //プレイヤーの移動用メソッド
    public void PlayerMove()
    {
        //左スティックの水平入力軸の値を取得する(右：プラス、左：マイナス)
        float horizontakInput = Input.GetAxis("Horizontal");

        //左スティックの垂直入力軸の値を取得する（上：プラス、下：マイナス）
        float verticalInput = Input.GetAxis("Vertical");

        //左スティックの入力から定義された座標に移動
        transform.localPosition += new Vector3(horizontakInput, 0, verticalInput) * m_moveSpeed * Time.deltaTime;
    }

    //プレイヤーの回転用メソッド
    public void PlayerRotate()
    {
        //右スティックの水平入力軸の値を取得する
        float horizontak2Input = Input.GetAxis("Horizontal2");

        //右スティックでプレイヤーを回転
        transform.Rotate(new Vector3(0, m_rotationSpeed * horizontak2Input, 0));
    }
}
