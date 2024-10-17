using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class HealBattery_Item : MonoBehaviour
{
    //ライトの処理をアタッチ
    public Player_Light Battery;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //回復の処理を呼び出す
            Battery.HealBattery();

            //消失
            Destroy(gameObject);
        }
    }
}
