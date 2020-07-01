using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : Stats<UnitStat>
{
    public UnitStats(JSONNode def) : base(def)
    {
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
