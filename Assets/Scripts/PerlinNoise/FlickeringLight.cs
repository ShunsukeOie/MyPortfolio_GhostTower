using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// パーリンノイズでランダムに光が揺らめくライト
[RequireComponent(typeof(Light))]

public class FlickeringLight : MonoBehaviour
{
    //パーリンノイズ生成用クラス
    private PerlinNoiseGenerator _bigNoiseGenerator = null, _smallNoiseGenerator = null;

    //揺らめく対象のライト
    private Light _light = null;

    [SerializeField, Header("LightのRangeの最小と最高")]
    private float _rangeMin = 1, _rangeMax = 2;

    [SerializeField, Header("ゆらめきの速さ(値が大きいほど早い)")]
    private float _rateOfChange = 1f;

    //=================================================================================
    //初期化
    //=================================================================================

    private void Start()
    {
        Init();
    }

    //実行中のInspectorの値が変えられた時に更新する用
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Init();
        }
    }

    //ノ初期化
    private void Init()
    {
        _light = GetComponent<Light>();

        _bigNoiseGenerator = new PerlinNoiseGenerator(_rangeMin * 0.9f, _rangeMax * 0.9f, _rateOfChange);
        _smallNoiseGenerator = new PerlinNoiseGenerator(_rangeMin * 0.1f, _rangeMax * 0.1f, _rateOfChange * 4);

        //最初の更新
        Update();
    }

    //=================================================================================
    //更新
    //=================================================================================

    private void Update()
    {
        _light.range = _bigNoiseGenerator.GetNoise() + _smallNoiseGenerator.GetNoise();
    }


}
