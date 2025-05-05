using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge_Battery : MonoBehaviour
{

    private float m_maxBattery = 100;     //�ő�d��
    private float m_currentBattery = 100; //���݂̓d��

    //�Q�[�W����N���X�̎擾
    [SerializeField] private GaugeController m_gaugeController;
    //�v���C���[�̃��C�g�̏��������Ă���N���X���擾
    [SerializeField] private Player_Light m_playerLight;

    private void Start()
    {
        //�Q�[�W�������l�ōX�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }

    //�d�͂̏���
    public void DamageButtonPush()
    {
        m_currentBattery -= 10;
        //�d�͂���������̃Q�[�W�̌����ڂ��X�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }

    //�A�C�e���œd�͂���
    public void HealButtonPush()
    {
        m_currentBattery += 10;
        //�d�͂��񕜂�����̃Q�[�W�̌����ڂ��X�V
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }
}
