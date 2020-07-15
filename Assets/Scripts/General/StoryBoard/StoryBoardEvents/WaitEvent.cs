using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitEvent : StoryBoardEvent
{
    float time;
    public WaitEvent(float time) {
        this.time = time;
    }

    public override bool IsFinished()
    {
        return time <= 0;
    }

    public override void Update(float dt)
    {
        time -= dt;
    }
}
