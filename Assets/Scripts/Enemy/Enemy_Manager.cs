using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    private Enemy_Search m_searchComp;
    private Enemy_Light m_lightComp;
    private Enemy_Vision m_visionComp;

    // �X�^�����Ԍv���p
    private float m_stanTime = 2.0f;
    private float m_stanTimer = 0.0f;

    // �O������ǂݎ��͏o���邪���������͏o���Ȃ�
    // �v���C���[��ǂ��Ă��邩�𔻒肷��ϐ�
    public bool m_isChasingPlayer { get; private set; } = false;

    // �X�^����Ԃ��ǂ����̃t���O
    public bool m_isStan { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�擾
        m_searchComp = GetComponent<Enemy_Search>();
        m_lightComp = GetComponent<Enemy_Light>();
        m_visionComp = GetComponent<Enemy_Vision>();
    }

    // Update is called once per frame
    void Update()
    {
        // �X�^�����Ă��Ȃ��Ƃ�
        if(!m_isStan)
        {
            // �|�C���g�Ɍ������Ĉړ�����
            m_searchComp.UpdateMove(m_isChasingPlayer);

            // �v���C���[�����E���ɂ��邩����
            m_visionComp.DetectPlayerInView();
        }
        else
        {
            // �X�^�����̈ړ����x��ύX
            m_searchComp.HandleStan(m_isStan);

            // ���Ԃ����Z����
            m_stanTimer += Time.deltaTime;
            // �^�C�}�[���X�^�����Ԃ𒴂�����
            if (m_stanTimer >= m_stanTime)
            {
                // ���Ԃ����Z�b�g����
                m_stanTimer = 0.0f;
                // �t���O���~�낷
                m_isStan = false;
                // �v���C���[��ǂ�Ȃ�����
                m_isChasingPlayer = false;
            }
        }
        // �v���C���[��ǂ��Ă��������
        m_lightComp.UpdateLighting(m_isChasingPlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�ɐG�ꂽ�珈������
        if (other.gameObject.tag == "Player")
        {
            // �����ʒu�ɖ߂�
            m_searchComp.ResetPosition();
            // �v���C���[��ǂ�Ȃ��悤��
            m_isChasingPlayer = false;
            // �G���ǂ��Ă���Ƃ��p�̉��ɐ؂�ւ���ׁAAudioManager�̃t���O��ς���
            AudioManager.Instance.UnregisterEnemyVision();
        }
    }

    // m_isChasingPlayer���Z�b�g����֐�
    public void SetChasePlayer(bool isChasing)
    {
        m_isChasingPlayer = isChasing;
    }

    // m_isStan���Z�b�g����֐�
    public void SetStan(bool isStan)
    {
        m_isStan = isStan;
    }

    // �v���C���[���A�C�e�����E�������Ɍ��点�邽�߂̊֐�
    public void TriggerFlash()
    {
        m_lightComp.FlashLight();
    }
}
