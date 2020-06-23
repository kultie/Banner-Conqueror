using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyTurnState : BattleStateBase
{
    protected override void OnEnter()
    {
        Debug.Log("Entering enemy turn");
        for (int i = 0; i < context.enemyParty.mainUnit.Length; i++)
        {
            context.battleController.AddCommandQueue(context.enemyParty.mainUnit[i], "attack");
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
