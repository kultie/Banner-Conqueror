using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Party
{
    public TeamSide team;
    public UnitEntity[] mainUnit;
    public BannerUnit bannerUnit;
    public Party(UnitEntity[] mainUnit, BannerUnit bannerUnit, TeamSide team)
    {
        this.mainUnit = mainUnit;
        this.bannerUnit = bannerUnit;
        this.team = team;
    }

    public bool Lost()
    {
        if (bannerUnit != null)
        {
            return bannerUnit.IsDead() || mainUnit.Count(a => !a.IsDead()) == 0;
        }
        else
        {
            return mainUnit.Count(a => !a.IsDead()) == 0;
        }
    }

    public void ResetAnimation()
    {
        for (int i = 0; i < mainUnit.Length; i++)
        {
            mainUnit[i].ResetAnimation();
        }
        bannerUnit.ResetAnimation();
    }

    public void InitUnits()
    {
        for (int i = 0; i < mainUnit.Length; i++)
        {
            mainUnit[i].Init();
        }
        bannerUnit.Init();
    }

}
