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
        //色情報を取得
        Color color = gameObject.GetComponent<Image>().color;
    }

    //ゲージの見た目を設定
    public void UpdateGauge(float current, float max)
    {
        _gaugeImage.fillAmount = current / max; //fillAmountを更新

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
