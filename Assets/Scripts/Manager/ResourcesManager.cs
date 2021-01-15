using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesManager
{
    static JSONNode formulaData;
    static JSONNode itemData;
    static JSONNode passiveData;

    static Dictionary<string, JSONNode> cachedFormulasData = new Dictionary<string, JSONNode>();
    static Dictionary<string, UnitScriptableObject> cachedUnitData = new Dictionary<string, UnitScriptableObject>();
    static Dictionary<string, JSONNode> cachedItemData = new Dictionary<string, JSONNode>();
    static Dictionary<string, JSONNode> cachedPassiveData = new Dictionary<string, JSONNode>();
    static Dictionary<string, AudioClip> cachedAudioClip = new Dictionary<string, AudioClip>();
    static Dictionary<string, Sprite> cachedSprite = new Dictionary<string, Sprite>();
    static Dictionary<string, Sprite[]> cachedSpriteSheet = new Dictionary<string, Sprite[]>();

    public static UnitScriptableObject GetUnitData(string id)
    {
        UnitScriptableObject data = null;
        if (!cachedUnitData.TryGetValue(id, out data))
        {
            UnitScriptableObject a = Resources.Load("GameData/Units/" + id) as UnitScriptableObject;
            data = a;
            cachedUnitData[id] = data;
        }
        return data;
    }

    public static JSONNode GetPassiveData(string id)
    {
        JSONNode data = null;
        if (!cachedPassiveData.TryGetValue(id, out data))
        {
            TextAsset a = Resources.Load("GameData/PassiveData") as TextAsset;
            passiveData = JSON.Parse(a.text);
            data = passiveData["passives"][id];
            cachedPassiveData[id] = data;
        }
        return data;
    }

    public static JSONNode GetItemData(string id)
    {
        JSONNode data = null;
        if (!cachedItemData.TryGetValue(id, out data))
        {
            TextAsset a = Resources.Load("GameData/ItemData") as TextAsset;
            itemData = JSON.Parse(a.text);
            data = itemData["items"][id];
            cachedItemData[id] = data;
        }
        return data;
    }

    public static AudioClip GetAudioClip(string id)
    {
        AudioClip clip = null;
        if (!cachedAudioClip.TryGetValue(id, out clip))
        {
            AudioClip a = Resources.Load("Sounds/" + id) as AudioClip;
            cachedAudioClip[id] = a;
            clip = a;
        }
        return clip;
    }

    public static Sprite GetSprite(string source)
    {
        Sprite result;
        if (!cachedSprite.TryGetValue(source, out result))
        {
            Sprite a = Resources.Load<Sprite>("RemoveFromProduct/Sprites/" + source);
            result = a;
            cachedSprite[source] = a;
        }
        return result;
    }

    public static Sprite GetSpriteFromSheet(string source, int index)
    {
        Sprite[] sheet;
        if (!cachedSpriteSheet.TryGetValue(source, out sheet))
        {
            Sprite[] a = Resources.LoadAll<Sprite>("RemoveFromProduct/Sprites/" + source);
            sheet = a;
            cachedSpriteSheet[source] = sheet;
        }
        return sheet[index];
    }

    public static Sprite[] GetSpritesSheet(string source)
    {
        Sprite[] sheet;
        if (!cachedSpriteSheet.TryGetValue(source, out sheet))
        {
            Sprite[] a = Resources.LoadAll<Sprite>("RemoveFromProduct/Sprites/" + source);
            sheet = a;
            cachedSpriteSheet[source] = sheet;
        }
        return sheet;
    }

    public static JSONNode GetFormula(string formulaType)
    {
        JSONNode result = null;
        if (!cachedFormulasData.TryGetValue(formulaType, out result))
        {
            TextAsset a = Resources.Load("GameData/GameFormula") as TextAsset;
            formulaData = JSON.Parse(a.text);
            result = formulaData["formulas"][formulaType];
            cachedFormulasData[formulaType] = result;
        }
        return result;
    }
}
