using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{
    public class ChangeHP : UnitBattleActionBase
    {
        [SerializeField]
        float amount;
        public override void OnUpdate(float dt)
        {
            ResolvingTarget(e =>
            {
                e.Heal(amount);
            });
        }
    }
}