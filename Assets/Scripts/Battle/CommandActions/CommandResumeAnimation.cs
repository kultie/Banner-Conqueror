using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandResumeAnimation : CommandAction
{
    public CommandResumeAnimation() : base()
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
        owner.display.ResumeAnimation();
    }
}
