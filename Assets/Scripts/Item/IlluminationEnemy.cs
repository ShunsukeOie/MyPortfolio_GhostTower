using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class IlluminationEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに当たったら処理する
        if (other.gameObject.tag == "Player")
        {
            // シーン内のエネミーを全取得する
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

            // エネミーの数分繰り返す
            for (int i = 0; i < enemy.Length; ++i)
            {
                // エネミーのスクリプトにアクセスしフラグを上げる
                enemy[i].GetComponent<Enemy>().TriggerFlash();
            }
            // アイテムゲットの音を流す
            AudioManager.Instance.ItemGetSE();

            // 自分自身を破壊する
            Destroy(gameObject);
        }
    }
}
