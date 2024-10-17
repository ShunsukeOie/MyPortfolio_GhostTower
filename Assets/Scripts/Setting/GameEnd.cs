using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    void Update()
    {
        //Escが押された時
        if (Input.GetKey(KeyCode.Escape))
        {
            End();
        }
    }

    public void End()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit(); //ゲームプレイ終了
#endif
    }
}
