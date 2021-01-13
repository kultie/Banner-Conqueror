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
    public JSONNode statsData;
    public UnitAbility[] abilities;
    public Dictionary<string, JSONNode> commands;
    public UnitStats stats;
    public UnitData(JSONNode data)
    {
        id = data["id"];
        sprites = ResourcesManager.GetSpritesSheet(data["sprite_source"]);
        foreach (KeyValuePair<string, JSONNode> kv in data["animation"].AsObject)
        {
            var data_frames = kv.Value["frames"].AsArray;
            Sprite[] anim_frames = new Sprite[data_frames.Count];
            for (int i = 0; i < anim_frames.Length; i++)
            {
                anim_frames[i] = sprites[data_frames[i].AsInt];
            }
            float data_spf = kv.Value["spf"].AsFloat;
            bool data_loop = kv.Value["loop"].AsBool;
            animations[kv.Key] = new AnimationData()
            {
                frames = anim_frames,
                spf = data_spf,
                loop = data_loop
            };
        }
        var banner_offset = data["banner_offset"].AsArray;
        var avatar_offset = data["avatar_offset"].AsArray;
        bannerOffset = new Vector2(banner_offset[0].AsFloat, banner_offset[1].AsFloat);
        avatarOffset = new Vector2(avatar_offset[0].AsFloat, avatar_offset[1].AsFloat);
        statsData = data["stats"];
        commands = GenerateUnitCommands(ResourcesManager.GetCommandData(data["commands"]));
    }

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
        abilities = data.abilities;
    }

    public AnimationData GetAnimation(string id)
    {
        return animations[id];
    }

    public Sprite GetSprite(int index)
    {
        return sprites[index];
    }

    private Dictionary<string, JSONNode> GenerateUnitCommands(JSONNode input)
    {
        Dictionary<string, JSONNode> result = new Dictionary<string, JSONNode>();
        foreach (KeyValuePair<string, JSONNode> kv in input.AsObject)
        {
            string key = kv.Key;
            result[key] = kv.Value;
        }
        return result;
    }
}