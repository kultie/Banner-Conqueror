using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Sirenix.OdinInspector;
[Serializable]
public class UnitStats
{
    [SerializeField]
    protected Dictionary<UnitStat, float> baseStats = new Dictionary<UnitStat, float>();

    protected Dictionary<object, string> statsModifiersSources = new Dictionary<object, string>();
    protected Dictionary<string, StatModifier> statsModifiers = new Dictionary<string, StatModifier>();
    private Dictionary<UnitStat, bool> dirtyStats = new Dictionary<UnitStat, bool>();
    public StatDictionary currentStats = new StatDictionary();

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


    public UnitStats(UnitStatsTemplate template)
    {
        baseStats[UnitStat.MaxHP] = template.maxHP;
        baseStats[UnitStat.MaxMP] = template.maxMP;
        dirtyStats[UnitStat.MaxHP] = true;
        dirtyStats[UnitStat.MaxMP] = true;
        foreach (var kv in template.values)
        {
            baseStats[kv.Key] = kv.Value;
            dirtyStats[kv.Key] = true;
        }
        baseStats[UnitStat.HP] = GetStats(UnitStat.MaxHP);
        baseStats[UnitStat.MP] = GetStats(UnitStat.MaxMP);
        dirtyStats[UnitStat.HP] = true;
        dirtyStats[UnitStat.MP] = true;

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
    public void AddModifier(string id, StatModifier statModifier)
    {
        statsModifiers[id] = statModifier;
        SetDirty(statModifier);
    }

    public void RemoveModifier(string id, StatModifier statModifier)
    {
        statsModifiers.Remove(id);
        SetDirty(statModifier);
    }

    public void ClearModifer()
    {
        statsModifiers.Clear();
        foreach (var kv in dirtyStats)
        {
            dirtyStats[kv.Key] = true;
        }
    }

    public float GetStats(UnitStat key)
    {
        if (!baseStats.ContainsKey(key)) return 0;
        if (dirtyStats[key])
        {
            float baseValue = baseStats[key];
            float flatValue = 0;
            float multValue = 0;
            var mods = statsModifiers.Values.ToArray();
            foreach (var data in mods)
            {
                if (data.flatValues.ContainsKey(key))
                {
                    flatValue += data.flatValues[key];
                }
                if (data.multValues.ContainsKey(key))
                {
                    multValue += data.multValues[key];
                }
            }
            float realValue = (baseValue + flatValue) * (1 + multValue);
            currentStats[key] = realValue;
            dirtyStats[key] = false;
        }
        return currentStats[key];
    }

    private void SetDirty(StatModifier mod)
    {
        foreach (var kv in mod.flatValues)
        {
            dirtyStats[kv.Key] = true;
        }
        foreach (var kv in mod.multValues)
        {
            dirtyStats[kv.Key] = true;
        }
    }

}
[Serializable]
public class UnitStatsTemplate
{
    public float maxHP;
    public float maxMP;
    public StatDictionary values;
}

[Serializable]
public class StatDictionary : UnitySerializedDictionary<UnitStat, float> { }
