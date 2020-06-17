using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandWait : CommandAction
{
    private float time;

    public CommandWait(Dictionary<string, object> args) : base(args)
    {
        time = (float)args["time"];
    }

    public override bool IsBlock()
    {
        return true;
    }

    public override bool IsFinished()
    {
        return time <= 0;
    }

    protected override void OnUpdate(float dt)
    {
        if (time > 0)
        {
            time -= dt;
        }
    }
}
