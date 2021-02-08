using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Battle
{
    public class Heal : ChangeHP
    {
        protected override void OnUpdate(float dt)
        {
            owner.Heal(GetValue(owner));
        }
    }
}