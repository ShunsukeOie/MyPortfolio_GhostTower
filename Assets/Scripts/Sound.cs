using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // ���i�[�p
    [SerializeField]
    private AudioClip[] _audio;

    // ����炷���߂ɕK�v�Ȃ��́i�X�s�[�J�[�j
    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = _audio[0];
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.clip = _audio[1];
            _audioSource.PlayOneShot(_audioSource.clip);
        }
       
    }
}
