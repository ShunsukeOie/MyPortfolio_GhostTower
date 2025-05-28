using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Lantern : MonoBehaviour
{
    // �ڂ̑O�ɂ��郉���^�����i�[����ׂ̕ϐ�
    private GameObject m_lanternObj = null;

    [SerializeField, Header("�ڂ̑O�ɃA�C�e�������鎞�ɕ\�������UI")]
    private GameObject m_UIImage;

    [SerializeField, Header("Ray�̃T�C�Y")]
    private Vector3 m_BoxSize = new Vector3(0.25f, 0.25f, 0.25f);

    // ���C�̔���p
    private RaycastHit m_hit;

    [SerializeField, Header("�ǂ̋����܂Ŕ������邩")]
    private float m_distance = 1.0f;
    
    // �A�C�e�����ڂ̑O�ɂ��邩
    private bool m_isItem = false;

    [SerializeField, Header("�ǂ̃��C���[�̔������邩")]
    private LayerMask m_layerMask;

    // �ڂ̑O�Ƀ����^�������邩�`�F�b�N����֐�
    public void LanternCheck()
    {
        // �ڂ̑O�ɔ��^�̃��C���΂��ă����^�������邩���肷��
        if(Physics.BoxCast(transform.position, m_BoxSize, transform.forward,
            out m_hit, gameObject.transform.rotation, m_distance, m_layerMask))
        {
            // ���C�Ƀq�b�g�����I�u�W�F�N�g�̃^�O��Lantern�������珈������
            if (m_hit.collider.gameObject.tag == "Lantern")
            {
                //�@�ڂ̑O�̃����^���̏����i�[
                m_lanternObj = m_hit.collider.gameObject;

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
            m_lanternObj = null;
            if (m_isItem)
            {
                //UI���\����
                m_UIImage.SetActive(false);
                m_isItem = false;
            }
        }
    }

    // �����^����_��������֐�
    public void LightUp()
    {
        // �����^���I�u�W�F��null����Ȃ������珈������
        if (m_lanternObj != null)
        {
            // �����^���I�u�W�F�̎q�I�u�W�F�N�g���擾����
            GameObject candle = m_lanternObj.transform.Find("candle_").gameObject;
            // �_�ł���߂�
            candle.GetComponent<FlickeringLight>().enabled = false;
            // ���͈̔͂�6�ɌŒ肷��
            candle.GetComponent<Light>().range = 6;
            // �����^���I�u�W�F�N�g���������Ȃ��悤���C���[��ς��Ă���
            m_lanternObj.layer = LayerMask.NameToLayer("Ignore Raycast");
            // UI���\���ɂ���
            m_UIImage.SetActive(false);

            // �����^���_���̉��𗬂�
            AudioManager.Instance.LanthanumSE();
        }
    }
}
