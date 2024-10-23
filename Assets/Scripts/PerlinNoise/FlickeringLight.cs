using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �p�[�����m�C�Y�Ń����_���Ɍ����h��߂����C�g
[RequireComponent(typeof(Light))]

public class FlickeringLight : MonoBehaviour
{
    //�p�[�����m�C�Y�����p�N���X
    private PerlinNoiseGenerator _bigNoiseGenerator = null, _smallNoiseGenerator = null;

    //�h��߂��Ώۂ̃��C�g
    private Light _light = null;

    [SerializeField, Header("Light��Range�̍ŏ��ƍō�")]
    private float _rangeMin = 1, _rangeMax = 2;

    [SerializeField, Header("���߂��̑���(�l���傫���قǑ���)")]
    private float _rateOfChange = 1f;

    //=================================================================================
    //������
    //=================================================================================

    private void Start()
    {
        Init();
    }

    //���s����Inspector�̒l���ς���ꂽ���ɍX�V����p
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Init();
        }
    }

    //�m������
    private void Init()
    {
        _light = GetComponent<Light>();

        _bigNoiseGenerator = new PerlinNoiseGenerator(_rangeMin * 0.9f, _rangeMax * 0.9f, _rateOfChange);
        _smallNoiseGenerator = new PerlinNoiseGenerator(_rangeMin * 0.1f, _rangeMax * 0.1f, _rateOfChange * 4);

        //�ŏ��̍X�V
        Update();
    }

    //=================================================================================
    //�X�V
    //=================================================================================

    private void Update()
    {
        _light.range = _bigNoiseGenerator.GetNoise() + _smallNoiseGenerator.GetNoise();
    }


}
