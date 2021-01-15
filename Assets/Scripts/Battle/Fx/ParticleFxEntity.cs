using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXEntity : FXEntity
{
    AnimationSystem anim;
    public ParticleFXEntity(Sprite[] sprites, EntityAnimationData data)
    {
        int[] _d = data.GetData();
        Sprite[] _s = new Sprite[_d.Length];
        for (int i = 0; i < _d.Length; i++)
        {
            _s[i] = sprites[_d[i]];
        }
        anim = new AnimationSystem(sprites, data.loop, data.spf);
    }

    public override void Update(float dt)
    {
        anim.Update(dt);
    }

    public bool FinishedAnim()
    {
        return anim.IsFinished();
    }

    public Sprite GetSprite()
    {
        return anim.Frame();
    }
}
