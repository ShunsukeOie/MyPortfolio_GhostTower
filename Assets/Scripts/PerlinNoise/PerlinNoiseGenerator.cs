using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseGenerator : MonoBehaviour
{
    //�p�[�����m�C�Y�̃V�[�h
    private float _seed = 0;

    //�m�C�Y�̍ŏ��A�ő�l
    private float _min = 0, _max = 1;

    //�m�C�Y�̕ω���
    private float _rateOfChange = 1f;

    //=================================================================================
    //������
    //=================================================================================

    public PerlinNoiseGenerator(float min = 0, float max = 1, float rateOfChange = 1)
    {
        //�V�[�h�̓����_���Ɍ���
        _seed = Random.Range(0f, 100f);

        _min = min;
        _max = max;

        _rateOfChange = rateOfChange;
    }

    /// <summary>
    /// �V�[�h��ݒ肷��
    /// </summary>
    public void SetSeed(float seed)
    {
        _seed = seed;
    }

    //=================================================================================
    //�擾
    //=================================================================================

    /// <summary>
    /// �p�[�����m�C�Y���擾(x�͎��Ԍo�߂ŕω��Ay�̓V�[�h�ŌŒ�)
    /// </summary>
    public float GetNoise()
    {
        return GetNoise(Time.time);
    }

    /// <summary>
    /// �p�[�����m�C�Y���擾(�C�ӂ�x�ł̒l�Ay�̓V�[�h�ŌŒ�)
    /// </summary>
    public float GetNoise(float x)
    {
        return GetNoise(x, _seed);
    }

    /// <summary>
    /// �p�[�����m�C�Y���擾
    /// </summary>
    public float GetNoise(float x, float y)
    {
        return _min + Mathf.PerlinNoise(x * _rateOfChange, y * _rateOfChange) * (-_min + _max);
    }

}
