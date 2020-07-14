using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public UnitEntity owner;
    public UnitEntity[] targets;
    public JSONNode costData;
    List<CommandAction> actions;
    public OnCommandFinished finishedCallback;
    public Command(UnitEntity owner, UnitEntity[] targets, string actionsID)
    {
        this.owner = owner;
        this.targets = targets;
        JSONArray actions = owner.data.commands[actionsID]["actions"].AsArray;
        if (owner.data.commands[actionsID]["cost"] != null)
        {
            costData = owner.data.commands[actionsID]["cost"];
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

    public void ReturnCostToOwner() {
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