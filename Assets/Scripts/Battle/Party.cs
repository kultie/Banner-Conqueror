using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party
{
    public TeamSide team;
    public UnitEntity[] mainUnit;
    public UnitEntity bannerUnit;
    public Party(UnitEntity[] mainUnit, UnitEntity bannerUnit, TeamSide team)
    {
        this.mainUnit = mainUnit;
        this.bannerUnit = bannerUnit;
        this.team = team;
    }
}
