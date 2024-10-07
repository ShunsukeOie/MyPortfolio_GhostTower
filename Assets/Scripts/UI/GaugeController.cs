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
        //�F�����擾
        Color color = gameObject.GetComponent<Image>().color;
    }

    //�Q�[�W�̌����ڂ�ݒ�
    public void UpdateGauge(float current, float max)
    {
        _gaugeImage.fillAmount = current / max; //fillAmount���X�V

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
