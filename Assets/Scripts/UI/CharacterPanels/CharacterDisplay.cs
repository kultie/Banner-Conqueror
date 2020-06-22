using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    public Text nameText;
    public Image portraitImage;
    public ResourceBar healthBar;
    public ResourceBar chargeBar;

    public void Init(UnitEntity unit)
    {
        nameText.text = unit.data.id;
        unit.onHealthChanged += OnHealthChange;
        unit.onChargeBarChanged += OnChargeBarChange;
        gameObject.SetActive(true);
    }

    void OnHealthChange(float currentValue, float maxValue)
    {
        healthBar.SetCurrentValue(currentValue, maxValue);
    }

    void OnChargeBarChange(float currentValue, float maxValue)
    {
        chargeBar.SetCurrentValue(currentValue, maxValue);
    }
}
