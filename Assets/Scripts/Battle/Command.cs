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
    List<CommandAction> actions;
    public OnCommandFinished finishedCallback;
    TargetType targetType;
    public Command(UnitEntity owner, UnitEntity[] targets, string actionsID, BattleController battleController)
    {
        this.owner = owner;
        this.targets = targets;
        this.battleController = battleController;
        RegisterCommandData(actionsID);
    }

    void RegisterCommandData(string id)
    {
        JSONNode commandData = owner.data.commands[id];
        JSONArray actions = commandData["actions"].AsArray;
        if (commandData["cost"] != null)
        {
            costData = commandData["cost"];
        }
        if (commandData["target_type"] != null)
        {
            targetType = Utilities.ConvertToEnum<TargetType>(commandData["target_type"]);
        }
        this.actions = new List<CommandAction>();
        for (int i = 0; i < actions.Count; i++)
        {
            CommandAction _act = null;
            if (actions[i]["type"].Value == "predefined")
            {
                _act = ActionDictionary.Get(actions[i]["name"].Value, actions[i]["args"]);
            }
            else
            {
                _act = new CommandAction(actions[i]);
            }

            this.actions.Add(_act);
        }
    }

    public void ResolveTarget()
    {
        if (targets == null)
        {
            targets = battleController.GetTarget(targetType, owner);
        }
        else if ((targetType == TargetType.SingleAlly || targetType == TargetType.SingleEnemy) && targets[0] != null)
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
        int deleteIndex = -1;
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Update(dt, owner, targets);
            if (actions[i].IsFinished())
            {
                deleteIndex = i;
                break;
            }
            if (actions[i].IsBlock())
            {
                break;
            }
        }
        if (deleteIndex != -1)
        {
            actions.RemoveAt(deleteIndex);
        }
    }

    public bool IsFinished()
    {
        return actions.Count == 0;
    }

    public void OnFinished()
    {
        finishedCallback?.Invoke(owner, targets);
        owner.ResetAnimation();
    }
}