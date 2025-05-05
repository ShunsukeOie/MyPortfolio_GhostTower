using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    // AudioSource�i�[�p
    private AudioSource _audioSource;

    // BGM�i�[�p
    [SerializeField, Header("BGM�̃I�u�W�F�N�g")]
    private GameObject[] _bgmObj;

    // �v���C���[�̉��i�[�p (0 = ���C�g�_��, 1 = ���C�g����, 2 = �t���b�V��, 3 = �����^���_��, 4 = ���ꉹ 5 = �A�C�e���Q�b�g)
    [SerializeField, Header("Player�p��SE")]
    private AudioClip[] _playerSE;

    // ���̂̓G���v���C���[�����F���Ă��邩���Ǘ�����J�E���^�[
    private int _visibleCount = 0;

    // ����؂�ւ���ׂ̃t���O
    private bool _changeAudio = false;

    private void Awake()
    {
        // �C���X�^���X���Ȃ���Ύ�����ݒ�
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            // ���łɂ���Ȃ�Ύ������폜����
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // �R���|�[�l���g���擾
        _audioSource = GetComponent<AudioSource>();
        // �ʏ�BGM�𗬂�
        ApplyBGMState(false);
    }

    private void ApplyBGMState(bool isChased)
    {
        if (_bgmObj.Length < 2) return;

        // �ʏ�BGM
        _bgmObj[0].SetActive(!isChased);
        // �G�ɒǂ��Ă���Ƃ���BGM
        _bgmObj[1].SetActive(isChased);
    }

    // �G���v���C���[�����F�������Ă΂��֐�
    public void RegisterEnemyVision()
    {
        _visibleCount++;
        if(!_changeAudio)
        {
            _changeAudio = true;
            ApplyBGMState(true);
        }        
    }

    // �G�����E����O�ꂽ���Ă΂��֐�
    public void UnregisterEnemyVision()
    {
        _visibleCount = Mathf.Max(_visibleCount - 1, 0);
        if(_visibleCount == 0 && _changeAudio)
        {
            _changeAudio = false;
            ApplyBGMState(false);
        }
    }

    // �v���C���[�����C�g��_�����鎞�ɗ������ʉ�
    public void LightUpSE()
    {
        // ���C�g�_���̉���炷
        _audioSource.PlayOneShot(_playerSE[0]);
    }

    // �v���C���[�����C�g���������鎞�ɗ������ʉ�
    public void LightDownSE()
    {
        // ���C�g�����̉���炷
        _audioSource.PlayOneShot(_playerSE[1]);
    }

    // �v���C���[���t���b�V�����g�p����Ƃ��ɗ������ʉ�
    public void FlashSE()
    {
        // �t���b�V���̉���炷
        _audioSource.PlayOneShot(_playerSE[2]);
    }

    // �v���C���[�������^���𓔂����ɗ������ʉ�
    public void LanthanumSE()
    {
        // �t���b�V���̉���炷
        _audioSource.PlayOneShot(_playerSE[3]);
    }

    // �v���C���[�����ꂽ���̂ɗ������ʉ�
    public void PlayerDeadSE()
    {
        // ���ꉹ��炷
        _audioSource.PlayOneShot(_playerSE[4]);
    }

    // �v���C���[���A�C�e������肵�����ɗ������ʉ�
    public void ItemGetSE()
    {
        // �A�C�e������̉��𗬂�
        _audioSource.PlayOneShot(_playerSE[5]);
    }
}
