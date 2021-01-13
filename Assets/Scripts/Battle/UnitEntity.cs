using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kultie.EventDispatcher;
public class UnitEntity : Entity
{
    public UnitDisplay display { protected set; get; }
    public UnitStats stats { protected set; get; }
    public UnitData data { protected set; get; }
    public string partyID { protected set; get; }
    public Party party { protected set; get; }

    public OnResourceValueChanged onHealthChanged;
    public OnResourceValueChanged onChargeBarChanged;

    public bool isPlayerUnit { protected set; get; }

    private BattleContext context;
    public UnitEntity(UnitData data)
    {
        this.data = data;
        stats = data.stats;
        stats.InitCurrentStats();
    }

    public void SetTurn(BattleContext c)
    {
        context = c;
    }

    public void SetPartyId(Party party, string value, bool isPlayerUnit)
    {
        this.party = party;
        this.isPlayerUnit = isPlayerUnit;
        partyID = value;
    }

    public virtual void SetDisplay(UnitDisplay display)
    {
        this.display = display;
        display.RequestAnimation(UnitAnimation.Idle.ToString());
    }

    protected override void OnUpdate(float dt)
    {

    }

    protected override void OnForceDestroy()
    {

    }

    public bool IsDead()
    {
        return stats.GetCurrentStats(UnitStat.HP) <= 0;
    }

    public void Stagger()
    {
        display.RequestAnimation(UnitAnimation.Hit.ToString());
        BattleController.Instance.timer.After(GameConfig.STAGGER_TIME, () =>
        {
            if (!IsDead())
                display.RequestAnimation(UnitAnimation.Idle.ToString());
        });
    }

    public void TakeDamage(float damage)
    {
        Stagger();
        float currentHP = stats.GetCurrentStats(UnitStat.HP);
        currentHP -= damage;
        UpdateHP(currentHP);
        if (IsDead())
        {
            Dead(context);
        }
    }

    public void Heal(float amount)
    {
        float currentHP = stats.GetCurrentStats(UnitStat.HP);
        currentHP += amount;
        UpdateHP(currentHP);
    }

    public void ChangeHP(float value)
    {
        float currentHP = stats.GetCurrentStats(UnitStat.HP);
        currentHP -= value;
        UpdateHP(currentHP);
        if (IsDead())
        {
            Dead(context);
        }
    }

    public virtual void ResetAnimation()
    {
        if (!IsDead())
            display.RequestAnimation(UnitAnimation.Idle.ToString());
    }

    public void Init()
    {
        EventDispatcher.CallEvent("update_hp_" + partyID, new Dictionary<string, object>()
        {
            {"current", stats.GetCurrentStats(UnitStat.HP) },
            {"max", stats.GetStats(UnitStat.MaxHP) }
        });
        EventDispatcher.CallEvent("update_mp_" + partyID, new Dictionary<string, object>()
        {
            {"current", stats.GetCurrentStats(UnitStat.MP) },
            {"max", stats.GetStats(UnitStat.MaxMP) }
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
        EventDispatcher.CallEvent("update_mp_" + partyID, new Dictionary<string, object>()
        {
            {"current",  stats.SetMP(value)},
            {"max", stats.GetStats(UnitStat.MaxMP) }
        });
    }

    public bool CheckCost(JSONNode costData)
    {
        bool result = true;

        foreach (KeyValuePair<string, JSONNode> kv in costData.AsObject)
        {
            float cost = kv.Value.AsFloat;
            UnitStat stat = Utilities.ConvertToEnum<UnitStat>(kv.Key);
            float currentValue = stats.GetCurrentStats(stat);
            if (cost > currentValue)
            {
                return false;
            }
        }

        return result;
    }

    public void LoseCost(JSONNode costData)
    {
        foreach (KeyValuePair<string, JSONNode> kv in costData.AsObject)
        {
            float cost = kv.Value.AsFloat;
            UnitStat stat = Utilities.ConvertToEnum<UnitStat>(kv.Key);
            float currentValue = stats.GetCurrentStats(stat);
            currentValue -= cost;
            switch (stat)
            {
                case UnitStat.HP:
                    UpdateHP(currentValue);
                    break;
                case UnitStat.MP:
                    UpdateMP(currentValue);
                    break;
            }
        }
    }

    public void GainCost(JSONNode costData)
    {
        foreach (KeyValuePair<string, JSONNode> kv in costData.AsObject)
        {
            float cost = kv.Value.AsFloat;
            UnitStat stat = Utilities.ConvertToEnum<UnitStat>(kv.Key);
            float currentValue = stats.GetCurrentStats(stat);
            currentValue += cost;
            switch (stat)
            {
                case UnitStat.HP:
                    UpdateHP(currentValue);
                    break;
                case UnitStat.MP:
                    UpdateMP(currentValue);
                    break;
            }
        }
    }

    public void Dead(BattleContext context)
    {
        display.RequestAnimation(UnitAnimation.Dead.ToString());
        context.storyBoard.AddToStoryBoard(new SpriteFadeEvent(this, 1f));
    }
}
