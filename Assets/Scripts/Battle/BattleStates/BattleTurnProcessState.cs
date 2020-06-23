using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnProcessState : BattleStateBase
{
    protected override void OnEnter()
    {
        Debug.Log("On turn process enter");
    }

    protected override void OnExit()
    {
        Debug.Log("Will run all post turn process here");
        context.playerParty.ResetAnimation();
        context.enemyParty.ResetAnimation();
    }

    protected override void OnUpdate(float dt)
    {
        if (context.BattleOver())
        {
            context.battleController.stateMachine.Change(BattleState.Result, context);
        }
        context.currentTurn.Update(dt);
        if (context.currentTurn.Finished())
        {
            context.ChangeParty();
        }
    }
}
