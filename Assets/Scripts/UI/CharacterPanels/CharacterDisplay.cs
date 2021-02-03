using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Kultie.EventDispatcher;
public class CharacterDisplay : MonoBehaviour
{
    public Text nameText;
    public Image portraitImage;
    public ResourceBar healthBar;
    public ResourceBar chargeBar;

    [SerializeField]
    AbilityContainer abilityContainer;
    UnitEntity unit;
    CharacterInput input;
    bool allowInput;
    private void Awake()
    {
        input = GetComponent<CharacterInput>();
    }

    public void Init(UnitEntity unit)
    {
        this.unit = unit;
        nameText.text = unit.data.id;
        EventDispatcher.RegisterEvent(BattleEvents.on_update_hp.ToString() + unit.partyID, OnHealthChange);
        EventDispatcher.RegisterEvent(BattleEvents.on_update_mp.ToString() + unit.partyID, OnChargeBarChange);
        EventDispatcher.RegisterEvent(BattleEvents.on_player_turn.ToString(), OnPlayerTurn);
        EventDispatcher.RegisterEvent(BattleEvents.on_player_turn_start.ToString(), OnPlayerTurnExecute);
        abilityContainer.Init(unit);
        gameObject.SetActive(true);
    }

    private void OnPlayerTurnExecute(Dictionary<string, object> obj)
    {
        allowInput = false;       
    }

    private void OnPlayerTurn(Dictionary<string, object> obj)
    {
        allowInput = true;
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
        if (unit is BannerUnit)
        {
            return;
        }

        if (allowInput)
        {
            if (input.Tap)
            {
                Debug.Log("attack");
                BattleController.Instance.AddCommandQueue(unit, 0);
            }
            else if (input.SwipeUp)
            {
                Debug.Log("Offensive");
                BattleController.Instance.AddCommandQueue(unit, 1);
            }
            else if (input.SwipeDown)
            {
                Debug.Log("Defensive");
                BattleController.Instance.AddCommandQueue(unit, 2);
            }
            input.ManualUpdate();
        }
        else input.ResetInput();
    }
}
