using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEntity : Entity
{
    public UnitDisplay display { private set; get; }
    public Stats stats { private set; get; }
    public UnitData data { private set; get; }
    public UnitEntity(UnitData data)
    {
        this.data = data;
        stats = new Stats(data.statsData);
    }

    public virtual void SetDisplay(UnitDisplay display)
    {
        this.display = display;
        display.RequestAnimation("idle");
    }

    protected override void OnUpdate(float dt)
    {

    }

    protected override void OnForceDestroy()
    {

    }
}
