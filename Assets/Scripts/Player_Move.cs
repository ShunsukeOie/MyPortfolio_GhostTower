using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //プレイヤーの移動速度
    [SerializeField]
    [Header("移動速度")] public float m_MoveSpeed = 0.15f;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //水平入力軸の値を取得する
        float horizontakInput = Input.GetAxis("Horizontal");

        //垂直入力軸の値を取得する
        float verticalInput = Input.GetAxis("Vertical");

        //入力された定義された座標に移動
        transform.Translate(new Vector3(horizontakInput,0,verticalInput) * m_MoveSpeed * Time.deltaTime)
    }
}
