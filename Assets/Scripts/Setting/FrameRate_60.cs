using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate_60 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // �t���[�����[�g��60�ɌŒ肷��
        Application.targetFrameRate = 60;
    }
}
