using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    void Update()
    {
        //Esc�������ꂽ��
        if (Input.GetKey(KeyCode.Escape))
        {
            End();
        }
    }

    public void End()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit(); //�Q�[���v���C�I��
#endif
    }
}
