using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GaugeController : MonoBehaviour
{

    [SerializeField] protected Image _gaugeImage;    //�Q�[�W�Ƃ��Ďg��Image


    // Start is called before the first frame update
    void Start()
    {
        //�F�̏�����
        gameObject.GetComponent<Image>().color = new Color(0.0f, 255.0f, 0.0f, 255.0f);
    }

    //�Q�[�W�̌����ڂ�ݒ�
    public void UpdateGauge(float current, float max)
    {
        _gaugeImage.fillAmount = current / max; //fillAmount���X�V

    }
    //�ΐF�ɂ���
    public void ChangeColor1()
    {
        gameObject.GetComponent<Image>().color = new Color(0.0f, 255.0f, 0.0f, 255.0f);
    }
    //���F�ɂ���
    public void ChangeColor2()
    {
        gameObject.GetComponent<Image>().color = new Color(10.0f, 5.0f, 0.0f, 255.0f);
    }
    //�ԐF�ɂ���
    public void ChangeColor3()
    {
        Debug.Log("3");
        gameObject.GetComponent<Image>().color = new Color(255.0f, 0.0f, 0.0f, 255.0f);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
