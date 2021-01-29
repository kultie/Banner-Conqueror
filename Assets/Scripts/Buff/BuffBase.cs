using BC.BattleEvent;
using Kultie.EventDispatcher;
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
    [SerializeField]
    BattleEventDictionary commands;
    int turnPassed;

    public virtual void OnAdd(UnitEntity owner)
    {
        turnPassed = 0;
        owner.AddStatModifer(id, modifers);
        ModifiEvents(true, owner);
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
        ModifiEvents(false, owner);
    }

    private void ModifiEvents(bool add, UnitEntity target)
    {
        foreach (var kv in commands)
        {
            string eventName = "";
            switch (kv.Key)
            {
                case BattleEvents.on_player_turn:
                case BattleEvents.on_player_turn_start:
                case BattleEvents.on_player_turn_end:
                case BattleEvents.on_enemy_turn_start:
                case BattleEvents.on_enemy_turn_end:
                    eventName = kv.Key.ToString();
                    break;
                case BattleEvents.on_receive_damage:
                case BattleEvents.on_deal_damage:
                case BattleEvents.on_update_hp:
                case BattleEvents.on_update_mp:
                case BattleEvents.on_active_buff:
                case BattleEvents.on_buff_expire:
                case BattleEvents.on_pre_ability:
                case BattleEvents.on_post_ability:
                case BattleEvents.on_battle_event:
                case BattleEvents.on_heal:
                    eventName = kv.Key.ToString() + target.partyID;
                    break;
            }
            for (int i = 0; i < kv.Value.events.Length; i++)
            {
                if (add)
                {
                    EventDispatcher.RegisterEvent(eventName, kv.Value.events[i].OnTrigger);
                }
                else
                {
                    EventDispatcher.UnRegisterEvent(eventName, kv.Value.events[i].OnTrigger);
                }
            }
        }
    }
}
