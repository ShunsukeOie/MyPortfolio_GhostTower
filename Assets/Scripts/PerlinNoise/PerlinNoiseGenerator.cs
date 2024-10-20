using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseGenerator : MonoBehaviour
{
    //パーリンノイズのシード
    private float _seed = 0;

    //ノイズの最小、最大値
    private float _min = 0, _max = 1;

    //ノイズの変化率
    private float _rateOfChange = 1f;

    //=================================================================================
    //初期化
    //=================================================================================

    public PerlinNoiseGenerator(float min = 0, float max = 1, float rateOfChange = 1)
    {
        //シードはランダムに決定
        _seed = Random.Range(0f, 100f);

        _min = min;
        _max = max;

        _rateOfChange = rateOfChange;
    }

    /// <summary>
    /// シードを設定する
    /// </summary>
    public void SetSeed(float seed)
    {
        _seed = seed;
    }

    //=================================================================================
    //取得
    //=================================================================================

    /// <summary>
    /// パーリンノイズを取得(xは時間経過で変化、yはシードで固定)
    /// </summary>
    public float GetNoise()
    {
        return GetNoise(Time.time);
    }

    /// <summary>
    /// パーリンノイズを取得(任意のxでの値、yはシードで固定)
    /// </summary>
    public float GetNoise(float x)
    {
        return GetNoise(x, _seed);
    }

    /// <summary>
    /// パーリンノイズを取得
    /// </summary>
    public float GetNoise(float x, float y)
    {
        return _min + Mathf.PerlinNoise(x * _rateOfChange, y * _rateOfChange) * (-_min + _max);
    }

}
