using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class HealBattery_Item : MonoBehaviour
{
    //���C�g�̏������A�^�b�`
    public Player_Light m_Battery;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //�񕜂̏������Ăяo��
            m_Battery.HealBattery();

            //����
            Destroy(gameObject);
        }
    }
}
