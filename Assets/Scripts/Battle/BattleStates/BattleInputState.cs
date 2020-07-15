using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInputState : BattleStateBase
{
    protected override void OnEnter()
    {
        context.SetLastState(BattleState.Input);
        if (!context.storyBoard.IsFinished()) {
            context.ChangeBattleState(BattleState.Event);
            return;
        }
        EventDispatcher.CallEvent("on_player_turn", null);
        context.SetTeam(context.playerParty);
        BattleUI.Instance.ShowExecuteButton(true);
        BattleUI.Instance.SetButtonFunction(Execute);
    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate(float dt)
    {

    }

    void Execute()
    {
        context.ExecuteTurn();
        EventDispatcher.CallEvent("on_player_turn_execute", null);
        BattleUI.Instance.ShowExecuteButton(false);
    }
}
