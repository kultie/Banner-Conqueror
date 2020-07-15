using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventState : BattleStateBase
{
    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate(float dt)
    {
        context.storyBoard.Update(dt);
        if (context.storyBoard.IsFinished())
        {
            context.ChangeToLastState();
        }
    }
}
