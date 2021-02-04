using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventState : GameStateBase
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
            context.ChangeGameState(GameState.Battle);
        }
    }
}
