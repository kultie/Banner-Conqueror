using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyDisplay : MonoBehaviour
{
    public Party party { private set; get; }
    public PartyFormation[] unitsFormation;
    public UnitDisplay bannerUnitDisplay;

    public void Setup(Party party)
    {
        PartyFormation formation = unitsFormation[party.members.Length - 1];
        if (party.bannerUnit != null)
        {
            bannerUnitDisplay.gameObject.SetActive(true);
            bannerUnitDisplay.SetUp(party.bannerUnit, party.team);
        }
        formation.gameObject.SetActive(true);
        formation.Setup(party.members, party.team);
    }
}
