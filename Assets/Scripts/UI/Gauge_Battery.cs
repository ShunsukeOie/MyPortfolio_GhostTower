using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge_Battery : MonoBehaviour
{

    private float m_maxBattery = 100;     //最大電力
    private float m_currentBattery = 100; //現在の電力

    //ゲージ操作クラスの取得
    [SerializeField] private GaugeController m_gaugeController;
    //プレイヤーのライトの処理をしているクラスを取得
    [SerializeField] private Player_Light m_playerLight;

    private void Start()
    {
        //ゲージを初期値で更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }

    //電力の消費
    public void DamageButtonPush()
    {
        m_currentBattery -= 10;
        //電力が減った後のゲージの見た目を更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }

    //アイテムで電力を回復
    public void HealButtonPush()
    {
        m_currentBattery += 10;
        //電力が回復した後のゲージの見た目を更新
        m_gaugeController.UpdateGauge(m_currentBattery, m_maxBattery);
    }
}
