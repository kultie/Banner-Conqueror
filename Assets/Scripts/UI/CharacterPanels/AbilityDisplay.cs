using Kultie.EventDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplay : MonoBehaviour
{
    UnitAbility ability;
    UnitEntity caster;
    [SerializeField]
    Text coolDownText;
    [SerializeField]
    Image abilityIcon;
    [SerializeField]
    GameObject abilityMask;
    public void Init(UnitAbility unitAbility, UnitEntity owner)
    {
        caster = owner;
        ability = unitAbility;
        abilityIcon.sprite = unitAbility.icon;
        coolDownText.text = unitAbility.CurrentCoolDown().ToString();
        coolDownText.gameObject.SetActive(!unitAbility.IsCooledDown());
        abilityMask.SetActive(!unitAbility.IsCooledDown());
    }

    private void OnEnable()
    {
        EventDispatcher.RegisterEvent(BattleEvents.on_player_turn.ToString(), OnPlayerTurn);
        EventDispatcher.RegisterEvent(BattleEvents.on_player_turn_start.ToString(), OnPlayerTurnStart);
    }

    private void OnPlayerTurnStart(Dictionary<string, object> obj)
    {
        EventDispatcher.UnRegisterEvent(BattleEvents.on_ability_cancel.ToString() + caster.partyID + ability.GetInstanceID(), OnAbilityCancel);
    }

    private void OnDisable()
    {
        EventDispatcher.UnRegisterEvent(BattleEvents.on_player_turn.ToString(), OnPlayerTurn);
        EventDispatcher.UnRegisterEvent(BattleEvents.on_player_turn_start.ToString(), OnPlayerTurnStart);
        EventDispatcher.UnRegisterEvent(BattleEvents.on_ability_cancel.ToString() + caster.partyID + ability.GetInstanceID(), OnAbilityCancel);
    }

    private void OnPlayerTurn(Dictionary<string, object> obj)
    {
        coolDownText.text = ability.CurrentCoolDown().ToString();
        coolDownText.gameObject.SetActive(ability.IsCooledDown());
        abilityMask.SetActive(!ability.IsCooledDown());
    }

    public void OnInteract()
    {
        EventDispatcher.RegisterEvent(BattleEvents.on_ability_cancel.ToString() + caster.partyID + ability.GetInstanceID(), OnAbilityCancel);
        abilityMask.SetActive(true);
        BattleController.Instance.AddCommandQueue(caster, ability);
    }

    private void OnAbilityCancel(Dictionary<string, object> obj)
    {
        abilityMask.gameObject.SetActive(false);
    }
}
