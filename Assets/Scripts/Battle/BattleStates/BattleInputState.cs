using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInputState : BattleStateBase
{
    protected override void OnEnter()
    {
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
        BattleUI.Instance.ShowExecuteButton(false);
    }
}
