using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kultie.Stats;
using System;

public class UnitStats : Stats<UnitStat>
{
    private static Dictionary<string, UnitStat> formularMap = new Dictionary<string, UnitStat>() {
        { "mhp", UnitStat.MaxHP},
        { "mmp", UnitStat.MaxMP},
        { "str", UnitStat.Strength},
        { "wis", UnitStat.Wisdom },
        { "def", UnitStat.Defend},
        { "mdef", UnitStat.MagicDefend},
        { "hp", UnitStat.HP},
        { "mp", UnitStat.MP},
    };

    public float GetStats(string key)
    {
        return GetStats(formularMap[key]);
    }
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
        baseStats[UnitStat.HP] = template.MaxHP;
        baseStats[UnitStat.MP] = template.MaxMP;
        Init();
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
