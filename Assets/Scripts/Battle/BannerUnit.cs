using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerUnit : UnitEntity
{
    public BannerUnit(UnitData data) : base(data)
    {

    }

    public override void SetDisplay(UnitDisplay display)
    {
        this.display = display;
        display.RequestAnimation(UnitAnimation.IdleBanner.ToString());
        display.unitBanner.transform.localPosition = data.bannerOffset;
    }

    protected override void OnForceDestroy()
    {

    }

    protected override void OnUpdate(float dt)
    {

    }

    public override void ResetAnimation()
    {
        display.RequestAnimation(UnitAnimation.IdleBanner.ToString());
    }

    public void TriggerBannerEffect(BattleContext battleContext)
    {
        Debug.Log("Banner effect lmao");
    }
}
