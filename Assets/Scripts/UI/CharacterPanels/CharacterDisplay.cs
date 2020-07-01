using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    public Text nameText;
    public Image portraitImage;
    public ResourceBar healthBar;
    public ResourceBar chargeBar;
    UnitEntity unit;
    Vector2 startDragPos;
    CharacterInput input;
    private void Awake()
    {
        input = GetComponent<CharacterInput>();
    }

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

    private void Update()
    {
        if (unit is BannerUnit) {
            return;
        }
        if (input.Tap)
        {
            BattleController.Instance.AddCommandQueue(unit, "attack");
        }
        else if (input.SwipeUp)
        {
            Debug.Log("Offensive");
            BattleController.Instance.AddCommandQueue(unit, "offensive");
        }
        else if (input.SwipeDown) {
            Debug.Log("Defensive");
            BattleController.Instance.AddCommandQueue(unit, "defensive");
        }
    }
}
