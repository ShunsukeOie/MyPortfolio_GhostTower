using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class HealBattery_Item : MonoBehaviour
{
    //���C�g�̏������A�^�b�`
    public Player_Light Battery;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //�񕜂̏������Ăяo��
            Battery.HealBattery();

            //����
            Destroy(gameObject);
        }
    }
}
