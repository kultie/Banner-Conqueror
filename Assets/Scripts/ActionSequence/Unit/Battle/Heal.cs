﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{
    public class Heal : UnitBattleActionBase
    {
        [SerializeField]
        float amount;
        public override void OnUpdate(float dt)
        {
            owner.Heal(amount);
        }
    }
}