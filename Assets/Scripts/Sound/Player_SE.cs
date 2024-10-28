using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player_SE
{
    // AudioSource格納用
    public static AudioSource _audioSource;

    // プレイヤーの音格納用 (0 = ライト点灯, 1 = ライト消灯, 2 = フラッシュ, 3 = ランタン点灯, 4 = やられ音 5 = アイテムゲット)
    public static AudioClip[] _playerse;

    // プレイヤーがライトを点灯する時に流す効果音
    public static void LightUpSE()
    {
        // ライト点灯の音を鳴らす
        _audioSource.PlayOneShot(_playerse[0]);
    }

    // プレイヤーがライトを消灯する時に流す効果音
    public static void LightDownSE()
    {
        // ライト消灯の音を鳴らす
        _audioSource.PlayOneShot(_playerse[1]);
    }

    // プレイヤーがフラッシュを使用するときに流す効果音
    public static void FlashSE()
    {
        // フラッシュの音を鳴らす
        _audioSource.PlayOneShot(_playerse[2]);
    }

    // プレイヤーがランタンを灯す時に流す効果音
    public static void LanthanumSE()
    {
        // フラッシュの音を鳴らす
        _audioSource.PlayOneShot(_playerse[3]);
    }

    // プレイヤーがやられた時のに流す効果音
    public static void PlayerDeadSE()
    {
        // やられ音を鳴らす
        _audioSource.PlayOneShot(_playerse[4]);
    }

    // プレイヤーがアイテムを入手した時に流す効果音
    public static void ItemGetSE()
    {
        // アイテム入手の音を流す
        _audioSource.PlayOneShot(_playerse[5]);
    }
}
