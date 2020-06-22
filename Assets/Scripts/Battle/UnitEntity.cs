using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEntity : Entity
{
    public UnitDisplay display { protected set; get; }
    public Stats stats { protected set; get; }
    public UnitData data { protected set; get; }

    public OnResourceValueChanged onHealthChanged;
    public OnResourceValueChanged onChargeBarChanged;
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

    public bool IsDead()
    {
        return stats.GetStats(UnitStat.HP) <= 0;
    }

    public void TakeDamage(float damage)
    {
        display.RequestAnimation("hit");
        BattleController.Instance.timer.After(.2f, () =>
        {
            display.RequestAnimation("idle");
        });
    }

    public virtual void ResetAnimation()
    {
        display.RequestAnimation("idle");
    }

    public void Init()
    {
        onHealthChanged?.Invoke(stats.GetStats(UnitStat.HP), stats.GetStats(UnitStat.MaxHP));
        onChargeBarChanged?.Invoke(stats.GetStats(UnitStat.MP), stats.GetStats(UnitStat.MaxMP));
    }
}
