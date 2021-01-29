using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kultie.EventDispatcher;
[Serializable]
public class UnitEntity : Entity
{
    public UnitDisplay display { protected set; get; }
    [SerializeField]
    protected UnitStats stats;
    public UnitData data { protected set; get; }
    public BuffContainer buffContainer { protected set; get; }

    internal void AddBuff(BuffBase buff)
    {
        buffContainer.AddBuff(buff);
    }

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
        buffContainer = new BuffContainer(this);
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
        return stats.GetStats(UnitStat.HP) <= 0;
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

    public void TakeDamage(float damage, UnitEntity damageDealer)
    {
        Stagger();
        BattleDamage bd = new BattleDamage()
        {
            attacker = damageDealer,
            defeneder = this,
            value = damage
        };

        EventDispatcher.CallEvent(BattleEvents.on_receive_damage.ToString() + partyID, new Dictionary<string, object> {
            { "battle_damage", bd},
        });

        float currentHP = stats.GetStats(UnitStat.HP);
        currentHP -= bd.value;
        UpdateHP(currentHP);

        if (IsDead())
        {
            Dead(context);
        }
        BattleController.Instance.CreateBattleDamageFX(Mathf.FloorToInt(damage), this);
    }

    public void Heal(float amount)
    {
        EventDispatcher.CallEvent(BattleEvents.on_heal.ToString() + partyID, null);
        float currentHP = stats.GetStats(UnitStat.HP);
        currentHP += amount;
        UpdateHP(currentHP);
    }

    public void ChangeHP(float value)
    {
        EventDispatcher.CallEvent(BattleEvents.on_update_hp.ToString() + partyID, null);
        float currentHP = stats.GetStats(UnitStat.HP);
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
        EventDispatcher.CallEvent(BattleEvents.on_update_hp.ToString() + partyID, new Dictionary<string, object>()
        {
            {"current", stats.GetStats(UnitStat.HP) },
            {"max", stats.GetStats(UnitStat.MaxHP) }
        });
        EventDispatcher.CallEvent(BattleEvents.on_update_mp.ToString() + partyID, new Dictionary<string, object>()
        {
            {"current", stats.GetStats(UnitStat.MP) },
            {"max", stats.GetStats(UnitStat.MaxMP) }
        });
    }

    public void UpdateHP(float value)
    {
        EventDispatcher.CallEvent(BattleEvents.on_update_hp.ToString() + partyID, new Dictionary<string, object>()
        {
            {"current",  stats.SetHP(value)},
            {"max", stats.GetStats(UnitStat.MaxHP) }
        });
    }

    public void UpdateMP(float value)
    {
        EventDispatcher.CallEvent(BattleEvents.on_update_mp.ToString() + partyID, new Dictionary<string, object>()
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
            float currentValue = stats.GetStats(stat);
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
            float currentValue = stats.GetStats(stat);
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
            float currentValue = stats.GetStats(stat);
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
        BattleController.Instance.AddEventToStoryBoard(new SpriteFadeEvent(this, 1f));
    }

    public void AddStatModifer(string id, params StatModifier[] mods)
    {
        for (int i = 0; i < mods.Length; i++)
        {
            stats.AddModifier(id, mods[i]);
        }
    }

    public float GetStats(UnitStat key)
    {
        return stats.GetStats(key);
    }
    public float GetStats(string key)
    {
        return stats.GetStats(key);
    }
}
