using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.ActionSequence.Unit.Battle
{
    public class GetStats : UnitBattleActionBase
    {
        [SerializeField]
        UnitStat stats;
        public override void OnUpdate(float dt)
        {

        }

        public override object GetValue()
        {
            return owner.GetStats(stats);
        }
    }
}

