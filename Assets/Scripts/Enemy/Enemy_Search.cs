using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Search : MonoBehaviour
{
    // ������͈�
    [SerializeField, Header("������͈�")]
    private float m_angle = 45.0f;

    // �͈͓��ɓ����Ă�����
    private void OnTriggerStay(Collider other)
    {
        // �^�O���v���C���[�����ʂ���
        if(other.gameObject.tag == "Player")
        {
            // ���ʂɑ΂��āA�v���C���[�̈ʒu���擾���A45�x�ȓ����Z�o
            Vector3 posDelta = other.transform.position - this.transform.position;
            // Angle()�֐��Ő��ʂɑ΂��ĉ��x�̊p�x�����擾����
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            // target_angle��m_angle�Ɏ��܂��Ă��邩�ǂ���
            if(target_angle < m_angle)
            {
                // ���C���g�p����targe�ɓ������Ă��邩���ʂ���
                if(Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit))
                {
                    // ���C�ɓ��������̂��v���C���[�������珈������
                    if(hit.collider == other)
                    {
                        Debug.Log("�����Ă���");
                    }
                }
            }
        }
    }
}
