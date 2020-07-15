using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyTurnState : BattleStateBase
{
    protected override void OnEnter()
    {
        context.SetPlayerCurrentTarget(null);
        context.SetLastState(BattleState.EnemyTurn);
        if (!context.storyBoard.IsFinished())
        {
            context.ChangeBattleState(BattleState.Event);
            return;
        }
        Debug.Log("Entering enemy turn");
        for (int i = 0; i < context.enemyParty.mainUnit.Length; i++)
        {
            context.enemyParty.mainUnit[i].variables["targets"] = new UnitEntity[] { context.playerParty.mainUnit[0] };
            context.battleController.AddCommandQueueAuto(context.enemyParty.mainUnit[i], "attack");
        }
        context.ExecuteTurn();
    }

    protected override void OnExit()
    {
        Debug.Log("Exitting enemy turn");
    }

    protected override void OnUpdate(float dt)
    {

    }
}
