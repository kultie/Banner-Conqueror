using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class AnimationData
{
    public Sprite[] frames;
    public float spf;
    public bool loop;
}
public class UnitData
{
    public string id;
    public Sprite[] sprites;
    public Dictionary<string, AnimationData> animations = new Dictionary<string, AnimationData>();
    public Vector2 bannerOffset;
    public Vector2 avatarOffset;
    public UnitAbility[] abilities;
    public UnitAbility attack;
    public UnitStats stats;

    public UnitData(UnitScriptableObject data)
    {
        id = data.name;
        sprites = data.sprites;
        foreach (var kv in data.animData)
        {
            string animData = kv.Value.data;
            int[] numbers = animData.Split(',').Select(Int32.Parse).ToArray();
            Sprite[] anim_frames = new Sprite[numbers.Length];
            for (int i = 0; i < anim_frames.Length; i++)
            {
                anim_frames[i] = sprites[numbers[i]];
            }
            animations[kv.Key.ToString()] = new AnimationData()
            {
                frames = anim_frames,
                spf = kv.Value.spf,
                loop = kv.Value.loop
            };
        }
        bannerOffset = data.bannerOffset;
        avatarOffset = data.avatarOffset;
        stats = new UnitStats(data.stats);
        abilities = new UnitAbility[data.abilities.Length];
        for (int i = 0; i < data.abilities.Length; i++)
        {
            abilities[i] = data.abilities[i].Clone();
        }
        attack = data.attack;
    }

    public AnimationData GetAnimation(string id)
    {
        return animations[id];
    }

    public Sprite GetSprite(int index)
    {
        return sprites[index];
    }
}