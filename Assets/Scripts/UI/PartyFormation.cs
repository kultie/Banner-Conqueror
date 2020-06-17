using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyFormation : MonoBehaviour
{
    public UnitDisplay[] units;

    public void Setup(UnitEntity[] unitEntity, TeamSide team)
    {
        for (int i = 0; i < unitEntity.Length; i++)
        {
            units[i].SetUp(unitEntity[i], team);
        }
    }
}
