using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kultie.Stats;
using System;

public class UnitStats : Stats<UnitStat>
{
    public UnitStats(JSONNode def) : base(def)
    {
    }

    public UnitStats(UnitStatsTemplate template) : base(null)
    {
        baseStats[UnitStat.MaxHP] = template.MaxHP;
        baseStats[UnitStat.MaxMP] = template.MaxMP;
        baseStats[UnitStat.Strength] = template.Strength;
        baseStats[UnitStat.Wisdom] = template.Wisdom;
        baseStats[UnitStat.Defend] = template.Defend;
        baseStats[UnitStat.MagicDefend] = template.MagDefend;
    }

    public void InitCurrentStats()
    {
        currentStats[UnitStat.HP] = GetStats(UnitStat.MaxHP);
        currentStats[UnitStat.MP] = GetStats(UnitStat.MaxMP);
    }

    public float SetHP(float value)
    {
        float maxHPValue = GetStats(UnitStat.MaxHP);
        currentStats[UnitStat.HP] = Mathf.Clamp(value, 0, maxHPValue);
        return currentStats[UnitStat.HP];
    }

    public float SetMP(float value)
    {
        float maxHPValue = GetStats(UnitStat.MaxMP);
        currentStats[UnitStat.MP] = Mathf.Clamp(value, 0, maxHPValue);
        return currentStats[UnitStat.MP];
    }
}
[Serializable]
public class UnitStatsTemplate
{
    public float MaxHP;
    public float MaxMP;
    public float Strength;
    public float Wisdom;
    public float Defend;
    public float MagDefend;
}
