using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.BattleEvent
{
    public class BattleLogEvent : BattleCommonEvent
    {
        [SerializeField]
        string value;
        public override void OnTrigger(Dictionary<string, object> args)
        {
            Debug.Log(value);
        }
    }
}