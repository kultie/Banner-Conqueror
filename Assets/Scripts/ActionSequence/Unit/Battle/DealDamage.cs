using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{
    public class DealDamage : ChangeHP
    {
        protected override void OnUpdate(float dt)
        {
            owner.TakeDamage(GetValue(owner), owner);
        }
    }
}