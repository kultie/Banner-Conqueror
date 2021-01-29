using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.BattleEvent
{
    public abstract class BattleCommonEvent : BattleEventListener
    {
        public override string DisplayOnEditor()
        {
            return "Common";
        }
    }
}