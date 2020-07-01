using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stats<T> where T : Enum
{
    protected Dictionary<T, float> stats = new Dictionary<T, float>();
    protected Dictionary<string, JSONNode> statsModifiers = new Dictionary<string, JSONNode>();
    protected Dictionary<T, float> currentStats = new Dictionary<T, float>();
    public Stats(JSONNode def)
    {
        foreach (KeyValuePair<string, JSONNode> s in def.AsObject)
        {
            T statsType = Utilities.ConvertToEnum<T>(s.Key);
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


    public float GetStats(T key)
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

    public float GetCurrentStats(T key)
    {
        return currentStats[key];
    }
}