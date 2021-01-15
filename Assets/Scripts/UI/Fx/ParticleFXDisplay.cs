using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXDisplay : FXEntityDisplay<ParticleFXEntity>
{
    public SpriteRenderer renderer;
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
