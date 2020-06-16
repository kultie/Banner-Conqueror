using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stats
{
    private Dictionary<UnitStat, float> stats = new Dictionary<UnitStat, float>();
    private Dictionary<string, JSONNode> statsModifiers = new Dictionary<string, JSONNode>();

    public Stats(JSONNode def)
    {
        foreach (KeyValuePair<string, JSONNode> s in def.AsObject)
        {
            UnitStat statsType = Utilities.ConvertToEnum<UnitStat>(s.Key);
            stats[statsType] = s.Value.AsFloat;
        }
    }

    public void AddModifier(string id, JSONNode modifier)
    {
        statsModifiers[id] = modifier;
    }

    public void RemoveModifier(string id)
    {
        statsModifiers.Remove(id);
    }

    public void ApplyEquipmentModifier(JSONNode weaponMod)
    {

    }

    public void ClearModifier()
    {
        statsModifiers.Clear();
    }

    public float GetStats(UnitStat key)
    {
        float baseValue = stats[key];
        float flatValue = 0;
        float multValue = 0;
        var mods = statsModifiers.Values.ToArray();
        foreach (var data in mods)
        {
            flatValue += data["flat"][key.ToString()].AsFloat;
            multValue += data["mult"][key.ToString()].AsFloat;
        }
        return (baseValue + flatValue) * (1 + multValue);
    }
}