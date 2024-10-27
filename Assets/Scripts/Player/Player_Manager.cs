using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    // スタート位置格納用
    private Vector3 startPos;

    [SerializeField, Header("AudioMangerオブジェクト")]
    private GameObject _audiomng;
    // AudioManagerのスクリプト格納用
    private AudioManager _audioscript;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        _audioscript = _audiomng.GetComponent<AudioManager>();

        // スタート位置を格納する
        startPos = transform.position;
    }



    private void OnCollisionEnter(Collision collision)
    {
        // タグがEnemyのオブジェクトに当たったら処理する
        if (collision.gameObject.tag == "Enemy")
        {
            // スタート位置に戻す
            transform.position = startPos;

            // やられ音を流す
            _audioscript.PlayerDeadSE();
        }

        // タグがItemのオブジェクトに当たったら処理する
        if(collision.gameObject.tag == "Item")
        {
            // アイテムゲットの音を流す
            _audioscript.ItemGetSE();
        }
    }
}
