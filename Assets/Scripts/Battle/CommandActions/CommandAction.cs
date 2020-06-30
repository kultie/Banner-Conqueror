using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAction
{
    public UnitEntity owner;
    public UnitEntity[] targets;
    protected bool hasStarted;
    private Dictionary<string, object> context;
    JSONNode data;
    public CommandAction(JSONNode data)
    {
        hasStarted = false;
        this.data = CommandInterpreter.ConvertCommand(data);
    }

    public void Update(float dt, UnitEntity owner, UnitEntity[] targets)
    {
        if (!hasStarted)
        {
            hasStarted = true;
            OnExecute(owner, targets);
            return;
        }
        OnUpdate(dt);
    }
    protected virtual void OnExecute(UnitEntity owner, UnitEntity[] targets)
    {
        JSONArray onExecute = data["on_execute"]?.AsArray;
        context = new Dictionary<string, object>();
        context["entity"] = owner;
        owner.variables["targets"] = targets;
        if (onExecute != null)
        {
            InterpreterBridge.RunCommands(onExecute, context);
        }
    }
    protected virtual void OnUpdate(float dt)
    {
        JSONArray onUpdate = data["on_update"]?.AsArray;
        if (onUpdate != null)
        {
            InterpreterBridge.RunCommands(onUpdate, context);
        }
    }
    public virtual bool IsFinished()
    {
        JSONNode isFinish = data["is_finish"];
        if (isFinish != null)
        {
            return (bool)InterpreterBridge.RunCommand(isFinish, context);
        }
        return true;
    }
    public virtual bool IsBlock()
    {
        JSONNode isBlock = data["is_block"];
        if (isBlock != null)
        {
            return (bool)InterpreterBridge.RunCommand(isBlock, context);
        }
        return false;
    }
}