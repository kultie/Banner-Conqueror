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
        Debug.Log("Entering enemy turn");
        for (int i = 0; i < context.enemyParty.members.Length; i++)
        {
            if (!context.enemyParty.members[i].IsDead())
            {
                context.enemyParty.members[i].variables["targets"] = null;
                context.battleController.AddCommandQueueAuto(context.enemyParty.members[i], 0);
            }
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
