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
        EventDispatcher.RegisterEvent("update_hp_" + unit.partyID, OnHealthChange);
        EventDispatcher.RegisterEvent("update_mp_" + unit.partyID, OnChargeBarChange);
        gameObject.SetActive(true);
    }

    void OnHealthChange(Dictionary<string, object> arg)
    {
        healthBar.SetCurrentValue((float)arg["current"], (float)arg["max"]);
    }

    void OnChargeBarChange(Dictionary<string, object> arg)
    {
        chargeBar.SetCurrentValue((float)arg["current"], (float)arg["max"]);
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
