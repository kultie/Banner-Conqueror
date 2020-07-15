using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Party
{
    public TeamSide team;
    public UnitEntity[] mainUnit;
    public BannerUnit bannerUnit;
    public Party(UnitEntity[] mainUnit, BannerUnit bannerUnit, TeamSide team)
    {
        this.mainUnit = mainUnit;
        for (int i = 0; i < mainUnit.Length; i++)
        {
            mainUnit[i].SetPartyId(team + i.ToString());
        }
        this.bannerUnit = bannerUnit;
        bannerUnit.SetPartyId(team + "banner");
        this.team = team;
    }

    public void SetBattleContext(BattleContext c)
    {
        for (int i = 0; i < mainUnit.Length; i++)
        {
            mainUnit[i].SetTurn(c);
        }
        bannerUnit.SetTurn(c);
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
