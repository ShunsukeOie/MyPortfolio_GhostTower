using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //プレイヤーの移動速度
    [SerializeField]
    [Header("移動速度")] public float MoveSpeed = 0.15f;

    //プレイヤーの回転スピード
    [SerializeField]
    [Header("回転スピード")] public float RotationSpeed = 0.15f;

    //Animatorの格納用
    //Animator m_animator;
   

    // Start is called before the first frame update
    void Start()
    {
        //アタッチされているコンポーネントを取得
        //m_animator = GetComponent<Animator>();
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
        //左スティックの水平入力軸の値を取得する(右：プラス、左：マイナス)
        float horizontakInput = Input.GetAxis("Horizontal");

        //左スティックの垂直入力軸の値を取得する（上：プラス、下：マイナス）
        float verticalInput = Input.GetAxis("Vertical");

        //左スティックの入力から定義された座標に移動
        transform.localPosition += new Vector3(horizontakInput, 0, verticalInput) * MoveSpeed * Time.deltaTime;


        //アニメーションの再生処理
        //上
        if(verticalInput > 0 && horizontakInput < 0.5 && horizontakInput < -0.5)
        {
            //m_animator.SetInteger("Action", 1);
        }
        //下
        else if(verticalInput < 0 && horizontakInput < 0.5 && horizontakInput < -0.5)
        {
            //m_animator.SetInteger("Action", 2);
        }
        //右
        else if(horizontakInput > 0 && verticalInput < 0.5 && verticalInput < -0.5)
        {
            //m_animator.SetInteger("Action", 3);
        }
        //左
        else if (horizontakInput < 0 && verticalInput < 0.5 && verticalInput < -0.5)
        {
            //m_animator.SetInteger("Action", 4);
        }
        //待機状態
        else
        {
           // m_animator.SetInteger("Action", 0);
        }
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
