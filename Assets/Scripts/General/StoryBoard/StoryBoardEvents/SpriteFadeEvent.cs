using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFadeEvent : StoryBoardEvent
{
    UnitEntity unit;
    float fadeTime;
    float time;
    public SpriteFadeEvent(UnitEntity unit, float fadeTime)
    {
        this.unit = unit;
        this.fadeTime = fadeTime;
        this.time = fadeTime;
    }
    public override bool IsFinished()
    {
        return fadeTime <= 0;
    }

    public override void Update(float dt)
    {
        fadeTime -= dt;
        unit.display.SetSpriteAlpha(fadeTime / time);
    }

    public override bool IsBlock()
    {
        return true;
    }
}
