using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleContext
{
    public Party playerParty;
    public Party enemyParty;
    public BattleContext(Party playerParty, Party enemyParty)
    {
        this.playerParty = playerParty;
        this.enemyParty = enemyParty;
    }
}
