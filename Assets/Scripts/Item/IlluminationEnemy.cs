using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class IlluminationEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

            for(int i = 0; i < enemy.Length; ++i)
            {
                enemy[i].GetComponent<Enemy_Light>().isLighting = true;
            }
            Destroy(gameObject);
        }
    }
}
