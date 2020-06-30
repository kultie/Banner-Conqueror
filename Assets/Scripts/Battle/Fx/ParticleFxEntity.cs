using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXEntity
{
    Sprite[] sprites;
    AnimationSystem anim;
    public ParticleFXEntity(JSONNode data)
    {
        var sheet = ResourcesManager.GetSpritesSheet(data["source"]);
        JSONArray framesData = data["frames"].AsArray;
        sprites = new Sprite[framesData.Count];
        for (int i = 0; i < framesData.Count; i++)
        {
            sprites[i] = sheet[framesData[i].AsInt];
        }
        float spf = data["spf"].AsFloat;
        anim = new AnimationSystem(sprites, false, spf);
    }

    public void Update(float dt)
    {
        anim.Update(dt);
    }

    public Sprite GetSprite() {
        return anim.Frame();
    }
}
