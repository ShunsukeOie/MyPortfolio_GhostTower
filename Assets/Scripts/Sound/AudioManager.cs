using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // AudioSource�i�[�p
    private AudioSource _audioSource;

    // BGM�i�[�p
    [SerializeField, Header("BGM�̃I�u�W�F�N�g")]
    private GameObject[] _bgmobj;

    // �v���C���[�̉��i�[�p (0 = ���C�g�_��, 1 = ���C�g����, 2 = �t���b�V��, 3 = �����^���_��, 4 = ���ꉹ 5 = �A�C�e���Q�b�g)
    [SerializeField, Header("Player�p��SE")]
    private AudioClip[] _playerse;

    // ����؂�ւ���ׂ̃t���O
    [HideInInspector]
    public bool ChangeAudio = false;
    

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g���擾
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // �t���O���������Ă�����f�t�H���g�̉��𗬂�
        if (!ChangeAudio)
        {
            // �G���ǂ��Ă���Ƃ��p��BGM�𗬂��Ȃ�
            _bgmobj[1].SetActive(false);

            // �f�t�H���g�̉��𗬂�
            _bgmobj[0].SetActive(true);
        }
        // �t���O���オ���Ă�����G���ǂ��Ă���Ƃ��p�̉��𗬂�
        else
        {
            // �f�t�H���g�̉��𗬂��Ȃ�
            _bgmobj[0].SetActive(false);

            // �G���ǂ��Ă���Ƃ��p�̉��𗬂�
            _bgmobj[1].SetActive(true);
        }
    }

    // �v���C���[�����C�g��_�����鎞�ɗ������ʉ�
    public void LightUpSE()
    {
        // ���C�g�_���̉���炷
        _audioSource.PlayOneShot(_playerse[0]);
    }

    // �v���C���[�����C�g���������鎞�ɗ������ʉ�
    public void LightDownSE()
    {
        // ���C�g�����̉���炷
        _audioSource.PlayOneShot(_playerse[1]);
    }

    // �v���C���[���t���b�V�����g�p����Ƃ��ɗ������ʉ�
    public void FlashSE()
    {
        // �t���b�V���̉���炷
        _audioSource.PlayOneShot(_playerse[2]);
    }

    // �v���C���[�������^���𓔂����ɗ������ʉ�
    public void LanthanumSE()
    {
        // �t���b�V���̉���炷
        _audioSource.PlayOneShot(_playerse[3]);
    }

    // �v���C���[�����ꂽ���̂ɗ������ʉ�
    public void PlayerDeadSE()
    {
        // ���ꉹ��炷
        _audioSource.PlayOneShot(_playerse[4]);
    }

    // �v���C���[���A�C�e������肵�����ɗ������ʉ�
    public void ItemGetSE()
    {
        // �A�C�e������̉��𗬂�
        _audioSource.PlayOneShot(_playerse[5]);
    }
}
