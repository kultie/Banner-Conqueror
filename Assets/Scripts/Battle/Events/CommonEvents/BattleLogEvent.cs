using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.BattleEvent
{
    public class BattleLogEvent : BattleEventListener
    {
        public override void OnTrigger(Dictionary<string, object> args)
        {
            Debug.Log("Hello world from battle log event");
        }
    }
}