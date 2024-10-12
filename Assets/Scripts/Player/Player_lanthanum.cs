using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_lanthanum : MonoBehaviour
{
    // �ڂ̑O�ɂ��郉���^�����i�[����ׂ̕ϐ�
    private GameObject LanthanumObj = null;

    RaycastHit hit;
    [SerializeField, Header("Ray�̃T�C�Y")]
    private Vector3 BoxSize;

    [SerializeField, Header("�ǂ̋����܂Ŕ������邩")]
    private float Distance;


    [Header("�ǂ̃��C���[�̔������邩")]
    public LayerMask layerMask;
    [SerializeField, Header("�ڂ̑O�ɃA�C�e�������鎞�ɕ\�������UI")]
    private GameObject UIImage;
    // �A�C�e�����ڂ̑O�ɂ��邩
    private bool IsItem = false;

    // Update is called once per frame
    void Update()
    {
        //�ڂ̑O�ɃA�C�e�������邩���`�F�b�N
        ItemCheck();

        // ���𓔂�
        LightUp();
    }

    void ItemCheck()
    {
        // �ڂ̑O�ɔ��^�̃��C���΂��ă����^�������邩���肷��
        if(Physics.BoxCast(transform.position, BoxSize, transform.forward,
            out hit, gameObject.transform.rotation, Distance, layerMask))
        {
            // ���C�Ƀq�b�g�����I�u�W�F�N�g�̃^�O��Lanthanum�������珈������
            if (hit.collider.gameObject.tag == "Lanthanum")
            {
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

    void LightUp()
    {
        //�@F�L�[�Ŏ擾
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(LanthanumObj != null)
            {
                LanthanumObj.transform.Find("candle_").gameObject.GetComponent<Light>().enabled = true;
                LanthanumObj.gameObject.tag = "none";
            }
        }
    }
}
