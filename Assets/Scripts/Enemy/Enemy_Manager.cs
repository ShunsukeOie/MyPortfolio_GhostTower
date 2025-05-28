using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    private Enemy_Search m_searchComp;
    private Enemy_Light m_lightComp;
    private Enemy_Vision m_visionComp;

    // スタン時間計測用
    private float m_stanTime = 2.0f;
    private float m_stanTimer = 0.0f;

    // 外部から読み取りは出来るが書き換えは出来ない
    // プレイヤーを追っているかを判定する変数
    public bool m_isChasingPlayer { get; private set; } = false;

    // スタン状態かどうかのフラグ
    public bool m_isStan { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネント取得
        m_searchComp = GetComponent<Enemy_Search>();
        m_lightComp = GetComponent<Enemy_Light>();
        m_visionComp = GetComponent<Enemy_Vision>();
    }

    // Update is called once per frame
    void Update()
    {
        // スタンしていないとき
        if(!m_isStan)
        {
            // ポイントに向かって移動する
            m_searchComp.UpdateMove(m_isChasingPlayer);

            // プレイヤーが視界内にいるか判定
            m_visionComp.DetectPlayerInView();
        }
        else
        {
            // スタン時の移動速度を変更
            m_searchComp.HandleStan(m_isStan);

            // 時間を加算する
            m_stanTimer += Time.deltaTime;
            // タイマーがスタン時間を超えたら
            if (m_stanTimer >= m_stanTime)
            {
                // 時間をリセットする
                m_stanTimer = 0.0f;
                // フラグを降ろす
                m_isStan = false;
                // プレイヤーを追わなくする
                m_isChasingPlayer = false;
            }
        }
        // プレイヤーを追っていたら光る
        m_lightComp.UpdateLighting(m_isChasingPlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに触れたら処理する
        if (other.gameObject.tag == "Player")
        {
            // 初期位置に戻す
            m_searchComp.ResetPosition();
            // プレイヤーを追わないように
            m_isChasingPlayer = false;
            // 敵が追っているとき用の音に切り替える為、AudioManagerのフラグを変える
            AudioManager.Instance.UnregisterEnemyVision();
        }
    }

    // m_isChasingPlayerをセットする関数
    public void SetChasePlayer(bool isChasing)
    {
        m_isChasingPlayer = isChasing;
    }

    // m_isStanをセットする関数
    public void SetStan(bool isStan)
    {
        m_isStan = isStan;
    }

    // プレイヤーがアイテムを拾った時に光らせるための関数
    public void TriggerFlash()
    {
        m_lightComp.FlashLight();
    }
}
