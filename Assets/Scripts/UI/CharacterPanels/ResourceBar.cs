using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Text valueText;
    public Image valueImage;

    public void SetCurrentValue(float currentValue, float maxValue)
    {
        valueText.text = currentValue + "/" + maxValue;
        valueImage.fillAmount = currentValue / maxValue;
    }
}
