using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnProcessState : BattleStateBase
{
    protected override void OnEnter()
    {
        context.SetLastState(BattleState.TurnProcess);
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
        if (context.storyBoard.IsFinished())
        {
            if (context.BattleOver())
            {
                context.ChangeBattleState(BattleState.Result);
                return;
            }

            if (context.currentTurn.Finished())
            {
                context.ChangeParty();
                return;
            }
        }

        if (context.currentTurn.CurrentCommandFinished())
        {
            if (!context.storyBoard.IsFinished())
            {
                Debug.Log("Running story board evet");
                context.ChangeBattleState(BattleState.Event);
                return;
            }
            context.currentTurn.ProcessNextCommand();
        }
        else
        {
            context.currentTurn.ProcessCurrentCommand(dt);
        }
    }
}
