using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAttack : CommandAction
{
    public CommandAttack(Dictionary<string, object> args = null) : base(args)
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
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].TakeDamage(0);
        }
    }
}
