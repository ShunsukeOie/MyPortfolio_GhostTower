using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Interactable : MonoBehaviour
{
    // �X�e�[�W2�{�^��
    [SerializeField]
    private GameObject stage2Button;

    // �X�e�[�W3�{�^��
    [SerializeField]
    private GameObject stage3Button;

    // Start is called before the first frame update
    [SerializeField]
    private void Start()
    {
        // �X�e�[�W1���N���A���Ă�����
        if (SceneController.stage1Clear)
        {
            // �X�e�[�W2�{�^�����N���b�N�ł���悤�ɂ���
            stage2Button.GetComponent<Button>().interactable = true;
        }
        // �X�e�[�W2���N���A���Ă�����
        if (SceneController.stage2Clear)
        {
            // �X�e�[�W3�{�^�����N���b�N�ł���悤�ɂ���
            stage3Button.GetComponent<Button>().interactable = true;
        }
    }
}
