using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{
    public class DealDamage : UnitBattleActionBase
    {
        [SerializeField]
        float amount;
        public override void OnUpdate(float dt)
        {
            owner.TakeDamage(amount);
        }
    }
}