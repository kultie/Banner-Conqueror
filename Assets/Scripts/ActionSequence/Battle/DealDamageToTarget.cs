﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Battle
{

    public class DealDamageToTarget : ChangeHP
    {
        protected override void OnUpdate(float dt)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].TakeDamage(GetValue(targets[i]), owner);
            }
        }
    }
}