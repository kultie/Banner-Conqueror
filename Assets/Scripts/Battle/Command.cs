using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Command
{
    private BattleController battleController;
    public UnitEntity owner;
    public UnitEntity[] targets;
    public JSONNode costData;
    public UnitAbility ability;
    public OnCommandFinished finishedCallback;
    TargetType targetType;
    public Command(UnitEntity owner, UnitEntity[] targets, UnitAbility ability, BattleController battleController)
    {
        this.owner = owner;
        this.targets = targets;
        this.battleController = battleController;
        this.ability = ability;
    }

    public void Execute()
    {
        ability.Execute(owner, targets);
    }

    public void ResolveTarget()
    {
        var targetType = ability.targetType;
        if (targets == null)
        {
            targets = battleController.GetTarget(targetType, owner);
        }
        else if (targetType == TargetType.OneEnemy && targets[0] != null)
        {
            if (targets[0].IsDead())
            {
                targets = battleController.GetTarget(targetType, owner);
            }
        }
        else
        {
            targets = battleController.GetTarget(targetType, owner);
        }
    }

    public void ReturnCostToOwner()
    {
        if (costData != null)
        {
            owner.GainCost(costData);
        }
    }

    public void Update(float dt)
    {
        ability.OnUpdate(dt);
    }

    public bool IsFinished()
    {
        return ability.IsFinished();
    }

    public void OnFinished()
    {
        finishedCallback?.Invoke(owner, targets);
        owner.ResetDisplay();
        //for (int i = 0; i < targets.Length; i++) {
        //    targets[i].ResetDisplay();
        //}
    }
}