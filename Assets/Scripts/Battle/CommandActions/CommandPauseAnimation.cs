using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPauseAnimation : CommandAction
{
    public CommandPauseAnimation() : base()
    {
    }

    public override bool IsBlock()
    {
        return false;
    }

    public override bool IsFinished()
    {
        return true;
    }

    protected override void OnUpdate(float dt)
    {
        owner.display.PauseAnimation();
    }
}
