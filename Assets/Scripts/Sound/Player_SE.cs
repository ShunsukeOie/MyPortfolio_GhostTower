using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player_SE
{
    // AudioSource�i�[�p
    public static AudioSource _audioSource;

    // �v���C���[�̉��i�[�p (0 = ���C�g�_��, 1 = ���C�g����, 2 = �t���b�V��, 3 = �����^���_��, 4 = ���ꉹ 5 = �A�C�e���Q�b�g)
    public static AudioClip[] _playerse;

    // �v���C���[�����C�g��_�����鎞�ɗ������ʉ�
    public static void LightUpSE()
    {
        // ���C�g�_���̉���炷
        _audioSource.PlayOneShot(_playerse[0]);
    }

    // �v���C���[�����C�g���������鎞�ɗ������ʉ�
    public static void LightDownSE()
    {
        // ���C�g�����̉���炷
        _audioSource.PlayOneShot(_playerse[1]);
    }

    // �v���C���[���t���b�V�����g�p����Ƃ��ɗ������ʉ�
    public static void FlashSE()
    {
        // �t���b�V���̉���炷
        _audioSource.PlayOneShot(_playerse[2]);
    }

    // �v���C���[�������^���𓔂����ɗ������ʉ�
    public static void LanthanumSE()
    {
        // �t���b�V���̉���炷
        _audioSource.PlayOneShot(_playerse[3]);
    }

    // �v���C���[�����ꂽ���̂ɗ������ʉ�
    public static void PlayerDeadSE()
    {
        // ���ꉹ��炷
        _audioSource.PlayOneShot(_playerse[4]);
    }

    // �v���C���[���A�C�e������肵�����ɗ������ʉ�
    public static void ItemGetSE()
    {
        // �A�C�e������̉��𗬂�
        _audioSource.PlayOneShot(_playerse[5]);
    }
}
