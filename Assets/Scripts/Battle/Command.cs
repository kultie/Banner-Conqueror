using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public UnitEntity owner;
    public UnitEntity[] targets;
    List<CommandAction> actions;
    public OnCommandFinished finishedCallback;
    public Command(UnitEntity owner, UnitEntity[] targets, string actionsID)
    {
        this.owner = owner;
        this.targets = targets;
        JSONArray actions = owner.data.commands[actionsID];
        this.actions = new List<CommandAction>();
        for (int i = 0; i < actions.Count; i++)
        {
            CommandAction _act = new CommandAction(actions[i]);
            this.actions.Add(_act);
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