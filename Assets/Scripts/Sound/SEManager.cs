using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    // AudioSource�i�[�p
    private AudioSource _audioSourceSE;

    // SE�i�[�p
    [SerializeField]
    private AudioClip _se;

    // �V���O���g��
    public static SEManager Instance
    {
        get; private set;
    }

    // �X�^�[�g�����D��G�ɏ�������
    private void Awake()
    {
        // SEManeger�����ɂ��鎞��Destroy����
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // SEManeger���Ȃ��Ƃ��͎��I�u�W�F�N�g��ێ�
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // �R���|�[�l���g���擾
        _audioSourceSE = this.GetComponent<AudioSource>();
    }

    // ����炷�ׂ̊֐�
    public void SettingPlaySE()
    {
        // ��x����SE�𗬂�
        _audioSourceSE.PlayOneShot(_se);
    }
}
