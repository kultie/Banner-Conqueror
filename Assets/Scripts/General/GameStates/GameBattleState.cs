using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameStateBase
{
    protected override void OnEnter()
    {
        Debug.Log("Entering batlte state");
    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate(float dt)
    {
        if (!context.storyBoard.IsFinished())
        {
            context.ChangeGameState(GameState.Event);
        }
        else
        {
            context.battleStateMachine.Update(dt);
        }
    }
}
