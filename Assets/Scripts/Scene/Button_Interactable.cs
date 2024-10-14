using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Interactable : MonoBehaviour
{
    // ステージ2ボタン
    [SerializeField]
    private GameObject stage2Button;

    // ステージ3ボタン
    [SerializeField]
    private GameObject stage3Button;

    // Start is called before the first frame update
    [SerializeField]
    private void Start()
    {
        // ステージ1をクリアしていたら
        if (SceneController.stage1Clear)
        {
            // ステージ2ボタンをクリックできるようにする
            stage2Button.GetComponent<Button>().interactable = true;
        }
        // ステージ2をクリアしていたら
        if (SceneController.stage2Clear)
        {
            // ステージ3ボタンをクリックできるようにする
            stage3Button.GetComponent<Button>().interactable = true;
        }
    }
}
