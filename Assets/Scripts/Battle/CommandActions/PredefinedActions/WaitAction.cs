using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WaitAction : CommandAction
{
    float currentTime;
    public WaitAction(JSONNode data) : base(data)
    {

    }

    protected override void OnExecute(UnitEntity owner, UnitEntity[] targets)
    {
        currentTime = data["time"].AsFloat;
    }

    protected override void OnUpdate(float dt)
    {
        currentTime -= dt;
    }

    public override bool IsBlock()
    {
        return true;
    }

    public override bool IsFinished()
    {
        return currentTime <= 0;
    }

    public static WaitAction Create(JSONNode data)
    {
        return new WaitAction(data);
    }

    public static WaitAction CreateStagger(JSONNode data)
    {
        JSONNode node = new JSONClass();
        node["time"] = GameConfig.STAGGER_TIME + 0.05f;
        return new WaitAction(node);
    }
}
