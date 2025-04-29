using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_lanthanum : MonoBehaviour
{
    // �ڂ̑O�ɂ��郉���^�����i�[����ׂ̕ϐ�
    private GameObject m_lanthanumObj = null;

    [SerializeField, Header("�ڂ̑O�ɃA�C�e�������鎞�ɕ\�������UI")]
    private GameObject m_UIImage;

    [SerializeField, Header("Ray�̃T�C�Y")]
    private Vector3 m_BoxSize;

    // ���C�̔���p
    private RaycastHit m_hit;

    [SerializeField, Header("�ǂ̋����܂Ŕ������邩")]
    private float m_distance;
    
    // �A�C�e�����ڂ̑O�ɂ��邩
    private bool m_isItem = false;

    [SerializeField, Header("�ǂ̃��C���[�̔������邩")]
    private LayerMask m_layerMask;

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
        if(Physics.BoxCast(transform.position, m_BoxSize, transform.forward,
            out m_hit, gameObject.transform.rotation, m_distance, m_layerMask))
        {
            // ���C�Ƀq�b�g�����I�u�W�F�N�g�̃^�O��Lanthanum�������珈������
            if (m_hit.collider.gameObject.tag == "Lanthanum")
            {
                //�@�ڂ̑O�̃����^���̏����i�[
                m_lanthanumObj = m_hit.collider.gameObject;

                if (!m_isItem)
                {
                    //UI��\��
                    m_UIImage.SetActive(true);
                    m_isItem = true;
                }
            }
        }
        else
        {
            //�ڂ̑O�ɖ���������
            //�A�C�e�������폜
            m_lanthanumObj = null;
            if (m_isItem)
            {
                //UI���\����
                m_UIImage.SetActive(false);
                m_isItem = false;
            }
        }
    }

    // �����^����_��������֐�
    void LightUp()
    {
        if (Input.GetButtonDown("LightUp"))
        {
            // �����^���I�u�W�F��null����Ȃ������珈������
            if(m_lanthanumObj != null)
            {
                // �����^���I�u�W�F�̎q�I�u�W�F�N�g���擾����
                GameObject candle = m_lanthanumObj.transform.Find("candle_").gameObject;
                // �_�ł���߂�
                candle.GetComponent<FlickeringLight>().enabled = false;
                // ���͈̔͂�6�ɌŒ肷��
                candle.GetComponent<Light>().range = 6;
                // �����^���I�u�W�F�N�g���������Ȃ��悤�^�O��؂�ւ��Ă���
                m_lanthanumObj.gameObject.tag = "none";
                // UI���\���ɂ���
                m_UIImage.SetActive(false);

                // �����^���_���̉��𗬂�
                AudioManager.instance.LanthanumSE();
            }
        }
    }
}
