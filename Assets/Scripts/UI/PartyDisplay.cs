using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyDisplay : MonoBehaviour
{
    public Party party { private set; get; }
    [SerializeField]
    PartyFormation[] unitsFormation;
    [SerializeField]
    UnitDisplay bannerUnitDisplay;
    private PartyFormation formation;

    public void Setup(Party party)
    {
        formation = unitsFormation[party.members.Length - 1];
        if (party.bannerUnit != null)
        {
            bannerUnitDisplay.gameObject.SetActive(true);
            bannerUnitDisplay.SetUp(party.bannerUnit, party.team);
        }
        formation.gameObject.SetActive(true);
        formation.Setup(party.members, party.team);
    }
}
