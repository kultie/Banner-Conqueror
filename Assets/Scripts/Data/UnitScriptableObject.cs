using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "BC/Create Unit")]
public class UnitScriptableObject : SerializedScriptableObject
{
    public UnitAbility attack;
    public Sprite[] sprites;
    public Dictionary<UnitAnimation, EntityAnimationData> animData;
    public Vector2 bannerOffset;
    public Vector2 avatarOffset;
    public UnitStatsTemplate stats;
    public UnitAbility[] abilities;
}
public enum UnitAnimation
{
    Idle,
    IdleBanner,
    Attack1,
    Attack2,
    Attack3,
    Hit,
    Dead
}
[Serializable]
public class EntityAnimationData
{
    public string data;
    public float spf = 0.12f;
    public bool loop;

    public int[] GetData()
    {
        return data.Split(',').Select(Int32.Parse).ToArray();
    }
}
