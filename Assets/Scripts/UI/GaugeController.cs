using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GaugeController : MonoBehaviour
{

    [SerializeField] protected Image _gaugeImage;    //ゲージとして使うImage


    // Start is called before the first frame update
    void Start()
    {
        //色の初期化
        gameObject.GetComponent<Image>().color = new Color(0.0f, 255.0f, 0.0f, 255.0f);
    }

    //ゲージの見た目を設定
    public void UpdateGauge(float current, float max)
    {
        _gaugeImage.fillAmount = current / max; //fillAmountを更新

    }
    //緑色にする
    public void ChangeColor1()
    {
        gameObject.GetComponent<Image>().color = new Color(0.0f, 255.0f, 0.0f, 255.0f);
    }
    //黄色にする
    public void ChangeColor2()
    {
        gameObject.GetComponent<Image>().color = new Color(10.0f, 5.0f, 0.0f, 255.0f);
    }
    //赤色にする
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
