using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.ActionSequence.Battle
{
    public class GetStats : UnitBattleActionBase
    {
        [SerializeField]
        UnitStat stats;

        public override object GetValue()
        {
            return owner.GetStats(stats);
        }
    }
}

