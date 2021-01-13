using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Party
{
    public TeamSide team;
    public UnitEntity[] members;
    public BannerUnit bannerUnit;
    public Party enemyParty;

    public Party(UnitEntity[] mainUnit, BannerUnit bannerUnit, TeamSide team, Party enemyParty)
    {
        this.members = mainUnit;
        for (int i = 0; i < mainUnit.Length; i++)
        {
            mainUnit[i].SetPartyId(this, team + i.ToString(), team == TeamSide.Player);
        }
        this.bannerUnit = bannerUnit;
        bannerUnit.SetPartyId(this, team + "banner", team == TeamSide.Player);
        this.team = team;
        this.enemyParty = enemyParty;
    }

    public void SetBattleContext(BattleContext c)
    {
        for (int i = 0; i < members.Length; i++)
        {
            members[i].SetTurn(c);
        }
        bannerUnit.SetTurn(c);
    }

    public bool Lost()
    {
        if (bannerUnit != null)
        {
            return bannerUnit.IsDead() || members.Count(a => !a.IsDead()) == 0;
        }
        else
        {
            return members.Count(a => !a.IsDead()) == 0;
        }
    }

    public void ResetAnimation()
    {
        for (int i = 0; i < members.Length; i++)
        {
            members[i].ResetAnimation();
        }
        bannerUnit.ResetAnimation();
    }

    public void InitUnits()
    {
        for (int i = 0; i < members.Length; i++)
        {
            members[i].Init();
        }
        bannerUnit.Init();
    }

    public UnitEntity GetRandom()
    {
        return members.Where(m => !m.IsDead()).Random();
    }
}
