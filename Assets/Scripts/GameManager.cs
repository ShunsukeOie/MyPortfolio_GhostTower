using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �������ԗp�ϐ�
    [SerializeField, Header("��������")]
    private float countdown;

    // ���Ԃ�\������e�L�X�g�i�[�p
    public Text timeText;

    // Update is called once per frame
    void Update()
    {
        // ���Ԃ����炵�Ă���
        countdown -= Time.deltaTime;

        // �e�L�X�g�Ɏ��Ԃ𔽉f������i�����ŏ����_�𒲐��ł���j
        timeText.text = countdown.ToString("F0");

        // ���Ԃ�0�ȉ��ɂȂ�����
        if(countdown <= 0)
        {
            // �Q�[���I�[�o�[�̃V�[�������[�h����
            SceneManager.LoadScene("GameOver");
        }
    }
}
