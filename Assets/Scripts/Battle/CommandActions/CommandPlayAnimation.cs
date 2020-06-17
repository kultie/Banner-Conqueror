using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPlayAnimation : CommandAction
{
    bool isBlock;
    bool started;
    bool waitForAnimToFinish;
    string animID;
    public CommandPlayAnimation(Dictionary<string, object> args) : base(args)
    {
        isBlock = (bool)args["block"];
        animID = (string)args["anim_id"];
        waitForAnimToFinish = false;
        if (args.ContainsKey("wait_for_anim"))
        {
            waitForAnimToFinish = (bool)args["wait_for_anim"];
        }


    }

    public override bool IsBlock()
    {
        return isBlock;
    }

    public override bool IsFinished()
    {
        if (!waitForAnimToFinish)
        {
            return true;
        }
        return owner.display.AnimFinished();
    }

    protected override void OnUpdate(float dt)
    {
        if (!started)
        {
            owner.display.RequestAnimation(animID);
            started = true;
        }
    }
}
