using Kultie.EventDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    [SerializeField]
    PartyDisplay partyDisplay;
    private void Awake()
    {
        EventDispatcher.RegisterEvent(BattleEvents.on_target_select.ToString(), OnTargetSelect);
    }

    private void OnTargetSelect(Dictionary<string, object> obj)
    {
        var unit = (UnitDisplay)obj["target"];
        transform.position = unit.transform.position;
    }
}
