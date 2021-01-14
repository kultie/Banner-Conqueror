using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{
    public class Heal : ChangeHP
    {
        public override void OnUpdate(float dt)
        {
            owner.Heal(GetValue(owner));
        }
    }
}