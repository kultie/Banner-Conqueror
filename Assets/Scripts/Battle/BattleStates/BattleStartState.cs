using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartState : BattleStateBase
{
    protected override void OnEnter()
    {
        context.playerParty.InitUnits();
        context.battleController.stateMachine.Change(BattleState.Input, context);
    }

    protected override void OnExit()
    {
       
    }

    protected override void OnUpdate(float dt)
    {

    }
}
