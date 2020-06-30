using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEntity : Entity
{
    public UnitDisplay display { protected set; get; }
    public Stats stats { protected set; get; }
    public UnitData data { protected set; get; }
    public string partyID { protected set; get; }

    public OnResourceValueChanged onHealthChanged;
    public OnResourceValueChanged onChargeBarChanged;
    public UnitEntity(UnitData data)
    {
        this.data = data;
        stats = new Stats(data.statsData);
        stats.InitCurrentStats();
    }

    public void SetPartyId(string value)
    {
        partyID = value;
    }

    public virtual void SetDisplay(UnitDisplay display)
    {
        this.display = display;
        display.RequestAnimation("idle");
    }

    protected override void OnUpdate(float dt)
    {

    }

    protected override void OnForceDestroy()
    {

    }

    public bool IsDead()
    {
        return stats.GetStats(UnitStat.HP) <= 0;
    }

    public void TakeDamage(float damage)
    {
        display.RequestAnimation("hit");
        float currentHP = stats.GetCurrentStats(UnitStat.HP);
        currentHP -= damage;
        UpdateHP(currentHP);
        BattleController.Instance.timer.After(.2f, () =>
        {
            display.RequestAnimation("idle");
        });
    }

    public virtual void ResetAnimation()
    {
        display.RequestAnimation("idle");
    }

    public void Init()
    {
        EventDispatcher.CallEvent("update_hp_" + partyID, new Dictionary<string, object>()
        {
            {"current", stats.GetCurrentStats(UnitStat.HP) },
            {"max", stats.GetStats(UnitStat.MaxHP) }
        });
    }

    public void UpdateHP(float value)
    {
        EventDispatcher.CallEvent("update_hp_" + partyID, new Dictionary<string, object>()
        {
            {"current",  stats.SetHP(value)},
            {"max", stats.GetStats(UnitStat.MaxHP) }
        });
    }

    public void UpdateMP(float value)
    {
        EventDispatcher.CallEvent("update_hp_" + partyID, new Dictionary<string, object>()
        {
            {"current",  stats.SetMP(value)},
            {"max", stats.GetStats(UnitStat.MaxMP) }
        });
    }
}
