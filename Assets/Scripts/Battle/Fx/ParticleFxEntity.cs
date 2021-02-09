using BC.ActionSequence.Effect;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXEntity : FXEntity
{
    AnimationSystem anim;
    public ParticleFXEntity(EffectAnimationData data)
    {
        int loopC = Mathf.Max(data.loopCount, 1);
        int totalLenght = data.sprites.Length * data.loopCount;
        Sprite[] _s = new Sprite[totalLenght];
        for (int i = 0; i < totalLenght; i++)
        {
            _s[i] = data.sprites[i % data.sprites.Length];
        }
        anim = new AnimationSystem(_s, false, data.spf);
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
