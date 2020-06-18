using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue
{
    public UnitEntity owner;
    public UnitEntity[] targets;
    List<CommandAction> actions;
    public CommandQueue(UnitEntity owner, UnitEntity[] targets, JSONArray actions)
    {
        this.owner = owner;
        this.targets = targets;
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
}