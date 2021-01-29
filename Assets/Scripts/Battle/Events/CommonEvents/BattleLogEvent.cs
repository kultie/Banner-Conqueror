using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.BattleEvent
{
    public class BattleLogEvent : BattleCommonEvent
    {
        public override void OnTrigger(Dictionary<string, object> args)
        {
            BattleDamage dmg = (BattleDamage)args["battle_damage"];
            Debug.Log(dmg.value);
        }
    }
}