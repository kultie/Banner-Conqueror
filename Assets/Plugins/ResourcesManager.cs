using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesManager
{
    static JSONNode bulletsData;
    static JSONNode enemyData;
    static JSONNode shipData;
    static JSONNode formulaData;
    static JSONNode itemData;
    static JSONNode passiveData;

    static Dictionary<string, JSONNode> cachedFormulasData = new Dictionary<string, JSONNode>();

    static Dictionary<string, JSONNode> cachedBulletsData = new Dictionary<string, JSONNode>();
    static Dictionary<string, JSONNode> cachedEnemyData = new Dictionary<string, JSONNode>();
    static Dictionary<string, JSONNode> cachedShipData = new Dictionary<string, JSONNode>();
    static Dictionary<string, JSONNode> cachedItemData = new Dictionary<string, JSONNode>();
    static Dictionary<string, JSONNode> cachedPassiveData = new Dictionary<string, JSONNode>();
    static Dictionary<string, AudioClip> cachedAudioClip = new Dictionary<string, AudioClip>();
    static Dictionary<string, Sprite> cachedSprite = new Dictionary<string, Sprite>();
    static Dictionary<string, Sprite[]> cachedSpriteSheet = new Dictionary<string, Sprite[]>();

    public static JSONNode GetBulletData(string id)
    {
        JSONNode data = null;
        if (!cachedBulletsData.TryGetValue(id, out data))
        {
            TextAsset a = Resources.Load("GameData/BulletsData") as TextAsset;
            bulletsData = JSON.Parse(a.text);
            data = bulletsData["bullets"][id];
            cachedBulletsData[id] = data;
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

    public static JSONNode GetShipData(string id)
    {
        JSONNode data = null;
        if (!cachedShipData.TryGetValue(id, out data))
        {
            TextAsset a = Resources.Load("GameData/ShipData") as TextAsset;
            shipData = JSON.Parse(a.text);
            data = shipData["ships"][id];
            cachedShipData[id] = data;
        }
        return data;
    }

    public static JSONNode GetEnemyData(string id)
    {
        JSONNode data = null;
        if (!cachedEnemyData.TryGetValue(id, out data))
        {
            TextAsset a = Resources.Load("GameData/EnemyData") as TextAsset;
            enemyData = JSON.Parse(a.text);
            data = enemyData["enemies"][id];
            cachedEnemyData[id] = data;
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
            Sprite a = Resources.Load<Sprite>(source);
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
            Sprite[] a = Resources.LoadAll<Sprite>(source);
            sheet = a;
            cachedSpriteSheet[source] = sheet;
        }
        return sheet[index];
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
