using Kultie.EventDispatcher;
using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTurnProcessState : BattleStateBase
{
    protected override void OnEnter()
    {
        context.SetLastState(BattleState.TurnProcess);
        if (context.currentTeam == TeamSide.Player)
        {
            EventDispatcher.CallEvent(BattleEvents.on_player_turn_start.ToString(), null);
        }
        else if (context.currentTeam == TeamSide.Enemy)
        {
            EventDispatcher.CallEvent(BattleEvents.on_enemy_turn_start.ToString(), null);
        }
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
            context.ChangeBattleState(BattleState.Result);
            return;
        }

        if (context.currentTurn.Finished())
        {
            if (context.currentTeam == TeamSide.Player)
            {
                EventDispatcher.CallEvent(BattleEvents.on_player_turn_end.ToString(), null);
            }
            else if (context.currentTeam == TeamSide.Enemy)
            {
                EventDispatcher.CallEvent(BattleEvents.on_enemy_turn_end.ToString(), null);
            }
            if (!BattleController.Instance.storyBoard.IsFinished())
            {
                return;
            }
            context.ChangeParty();
            return;
        }

        if (context.currentTurn.CurrentCommandFinished())
        {
            if (!BattleController.Instance.storyBoard.IsFinished())
            {
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
