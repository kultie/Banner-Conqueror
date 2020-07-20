using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultState : BattleStateBase
{
    protected override void OnEnter()
    {
        Debug.Log(context.battleResult);
    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate(float dt)
    {

    }
}
