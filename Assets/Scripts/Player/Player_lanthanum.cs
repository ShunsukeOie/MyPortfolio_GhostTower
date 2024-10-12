using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_lanthanum : MonoBehaviour
{
    [SerializeField]
    private GameObject ItemObj;

    RaycastHit hit;
    [SerializeField, Header("Ray�̃T�C�Y")]
    private Vector3 BoxSize;

    [SerializeField,  Header("�ǂ̈ʒu����Ray���΂���")]
    private Vector3 AddPos;

    [SerializeField, Header("�ǂ̋����܂Ŕ������邩")]
    private float Distance = 2f;


    [Header("�ǂ̃��C���[�̔������邩")]
    public LayerMask layerMask;
    [SerializeField, Header("�ڂ̑O�ɃA�C�e�������鎞�ɕ\�������UI")]
    private GameObject UIImage;
    // �A�C�e�����ڂ̑O�ɂ��邩
    private bool IsItem = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�ڂ̑O�ɃA�C�e�������邩���`�F�b�N
        ItemCheck();

        //�@F�L�[�Ŏ擾
        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }

    void ItemCheck()
    {
        //�ڂ̑O�ɔ��^��Ray���΂��ā@�A�C�e�������邩��T��
        if (Physics.BoxCast(gameObject.transform.position + AddPos, BoxSize, transform.forward,
            out hit, gameObject.transform.rotation, Distance, layerMask))
        {
            //�@�ڂ̑O�̃A�C�e���̏����i�[
            ItemObj = hit.collider.gameObject;
            if (!IsItem)
            {
                //UI��\��
                UIImage.SetActive(true);
                IsItem = true;
            }
        }
        else
        {
            //�ڂ̑O�ɖ���������
            //�A�C�e�������폜
            ItemObj = null;
            if (IsItem)
            {
                //UI���\����
                UIImage.SetActive(false);
                IsItem = false;
            }
        }
    }
}
