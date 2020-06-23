using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour, IPointerClickHandler
{
    public Text nameText;
    public Image portraitImage;
    public ResourceBar healthBar;
    public ResourceBar chargeBar;
    UnitEntity unit;

    public void Init(UnitEntity unit)
    {
        this.unit = unit;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (unit is BannerUnit)
        {
            return;
        }
        BattleUI.Instance.AddCommandToStack(null, BattleController.Instance.AddCommandQueue(unit, "attack"));
    }
}
