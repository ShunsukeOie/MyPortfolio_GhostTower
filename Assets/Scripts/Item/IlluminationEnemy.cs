using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class IlluminationEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�ɓ��������珈������
        if (other.gameObject.tag == "Player")
        {
            // �V�[�����̃G�l�~�[��S�擾����
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

            // �G�l�~�[�̐����J��Ԃ�
            for (int i = 0; i < enemy.Length; ++i)
            {
                // �G�l�~�[�̃X�N���v�g�ɃA�N�Z�X���t���O���グ��
                enemy[i].GetComponent<Enemy>().TriggerFlash();
            }
            // �A�C�e���Q�b�g�̉��𗬂�
            AudioManager.Instance.ItemGetSE();

            // �������g��j�󂷂�
            Destroy(gameObject);
        }
    }
}
