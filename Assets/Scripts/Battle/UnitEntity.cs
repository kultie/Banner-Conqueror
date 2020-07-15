using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEntity : Entity
{
    public UnitDisplay display { protected set; get; }
    public UnitStats stats { protected set; get; }
    public UnitData data { protected set; get; }
    public string partyID { protected set; get; }

    public OnResourceValueChanged onHealthChanged;
    public OnResourceValueChanged onChargeBarChanged;

    private BattleContext context;
    public UnitEntity(UnitData data)
    {
        this.data = data;
        stats = new UnitStats(data.statsData);
        stats.InitCurrentStats();
    }

    public void SetTurn(BattleContext c)
    {
        context = c;
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
        if (IsDead())
        {
            Dead(context);
        }
        BattleController.Instance.timer.After(GameConfig.STAGGER_TIME, () =>
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
        context.storyBoard.AddToStoryBoard(new SpriteFadeEvent(this, 1f));
    }
}
