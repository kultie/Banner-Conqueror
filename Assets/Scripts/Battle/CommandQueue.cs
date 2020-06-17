using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue
{
    public UnitEntity owner;
    public UnitEntity[] targets;
    List<CommandAction> actions;
    public CommandQueue(UnitEntity owner, UnitEntity[] targets, List<CommandAction> actions)
    {
        this.owner = owner;
        this.actions = actions;
        this.targets = targets;
    }
    public void Execute()
    {

    }
    public void Update(float dt)
    {
        int deleteIndex = -1;
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Setup(owner, targets);
            actions[i].Update(dt);
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