using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXDisplay : FXEntityDisplay<ParticleFXEntity>
{
    Sprite[] sprites;
    public SpriteRenderer renderer;
    AnimationSystem anim;
    protected override void OnUpdate(float dt)
    {
        entity.Update(dt);
        renderer.sprite = entity.GetSprite();
        if (entity.FinishedAnim())
        {
            gameObject.Recycle();
        }
    }
}
