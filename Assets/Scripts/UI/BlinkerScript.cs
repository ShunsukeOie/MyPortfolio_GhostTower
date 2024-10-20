using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkerScript : MonoBehaviour
{
    public float speed = 1.0f;
    private float time;
    private Text text;

    void Start()
    {
        //�e�L�X�g�̏����擾
        text = this.gameObject.GetComponent<Text>();
    }

    void Update()
    {
        //�_�ŏ����̌Ăяo��
        text.color = GetTextColorAlpha(text.color);
    }

    //�e�L�X�g��_�ł�����
    Color GetTextColorAlpha(Color color)
    {
        time += Time.deltaTime * speed * 5.0f;
        color.a = Mathf.Sin(time);

        return color;
    }
}
