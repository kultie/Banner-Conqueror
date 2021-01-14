using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BC/Create Buff")]
public class BuffBase : ScriptableObject
{
    public enum BuffType { Buff, Debuff, Passive };
    [SerializeField]
    BuffType buffType;
    public string id;
    public int order;
    [ShowIf("@buffType != BuffType.Passive")]
    public int duration;
    public StatModifier[] modifers;

    int turnPassed;

    public virtual void OnAdd(UnitEntity owner)
    {
        turnPassed = 0;
        owner.AddStatModifer(id, modifers);
    }

    public virtual void ProcessTurn(UnitEntity owner)
    {
        turnPassed++;
        OnProcessTurn(owner);
    }
    public void OnProcessTurn(UnitEntity owner) { }
    public void OnExpired(UnitEntity owner) { }
    public virtual bool Expired()
    {
        if (buffType == BuffType.Passive) return false;
        return turnPassed >= duration;
    }

    internal void OnRemove(UnitEntity owner)
    {

    }
}
