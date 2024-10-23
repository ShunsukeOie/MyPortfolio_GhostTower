using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_lanthanum : MonoBehaviour
{
    // �ڂ̑O�ɂ��郉���^�����i�[����ׂ̕ϐ�
    private GameObject LanthanumObj = null;

    [SerializeField, Header("�ڂ̑O�ɃA�C�e�������鎞�ɕ\�������UI")]
    private GameObject UIImage;

    [SerializeField, Header("Ray�̃T�C�Y")]
    private Vector3 BoxSize;

    // ���C�̔���p
    private RaycastHit hit;

    [SerializeField, Header("�ǂ̋����܂Ŕ������邩")]
    private float Distance;
    
    // �A�C�e�����ڂ̑O�ɂ��邩
    private bool IsItem = false;

    [Header("�ǂ̃��C���[�̔������邩")]
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        //�ڂ̑O�Ƀ����^�������邩���`�F�b�N
        LanthanumCheck();

        // ���𓔂�
        LightUp();
    }

    // �ڂ̑O�Ƀ����^�������邩�`�F�b�N����֐�
    void LanthanumCheck()
    {
        // �ڂ̑O�ɔ��^�̃��C���΂��ă����^�������邩���肷��
        if(Physics.BoxCast(transform.position, BoxSize, transform.forward,
            out hit, gameObject.transform.rotation, Distance, layerMask))
        {
            // ���C�Ƀq�b�g�����I�u�W�F�N�g�̃^�O��Lanthanum�������珈������
            if (hit.collider.gameObject.tag == "Lanthanum")
            {
                Debug.Log("����");
                Debug.Log(hit.collider.gameObject.name);
                //�@�ڂ̑O�̃����^���̏����i�[
                LanthanumObj = hit.collider.gameObject;

                if (!IsItem)
                {
                    //UI��\��
                    UIImage.SetActive(true);
                    IsItem = true;
                }
            }
        }
        else
        {
            //�ڂ̑O�ɖ���������
            //�A�C�e�������폜
            LanthanumObj = null;
            if (IsItem)
            {
                //UI���\����
                UIImage.SetActive(false);
                IsItem = false;
            }
        }
    }

    // �����^����_��������֐�
    void LightUp()
    {
        if (Input.GetButtonDown("LightUp"))
        {
            // �����^���I�u�W�F��null����Ȃ������珈������
            if(LanthanumObj != null)
            {
                // �����^���I�u�W�F�̎q�I�u�W�F�N�g���擾����
                GameObject candle = LanthanumObj.transform.Find("candle_").gameObject;
                // �_�ł���߂�
                candle.GetComponent<FlickeringLight>().enabled = false;
                // ���͈̔͂�6�ɌŒ肷��
                candle.GetComponent<Light>().range = 6;
                // �����^���I�u�W�F�N�g���������Ȃ��悤�^�O��؂�ւ��Ă���
                LanthanumObj.gameObject.tag = "none";
                // UI���\���ɂ���
                UIImage.SetActive(false);
            }
        }
    }
}
