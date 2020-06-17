using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSetFrame : CommandAction
{
    int spriteFrame;
    public CommandSetFrame(Dictionary<string, object> args) : base(args)
    {
        spriteFrame = (int)args["sprite_index"];
    }

    public override bool IsBlock()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsFinished()
    {
        return true;
    }

    protected override void OnUpdate(float dt)
    {
        owner.display.SetSprite(owner.data.GetSprite(spriteFrame));
    }
}
