using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandAction
{
    public UnitEntity owner;
    public UnitEntity[] targets;
    protected Dictionary<string, object> args;
    protected bool hasStarted;
    public CommandAction(Dictionary<string, object> args = null)
    {
        hasStarted = false;
        this.args = args;
    }

    public void Setup(UnitEntity owner, UnitEntity[] targets)
    {
        this.owner = owner;
        this.targets = targets;
    }
    public void Update(float dt)
    {
        if (!hasStarted)
        {
            hasStarted = true;
        }
        OnUpdate(dt);
    }
    protected abstract void OnUpdate(float dt);
    public abstract bool IsFinished();
    public abstract bool IsBlock();
}