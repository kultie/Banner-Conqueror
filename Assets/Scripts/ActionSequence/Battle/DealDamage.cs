using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Battle
{
    public class DealDamage : ChangeHP
    {
        protected override void OnUpdate(float dt)
        {
            owner.TakeDamage(GetValue(owner), owner);
        }
    }
}