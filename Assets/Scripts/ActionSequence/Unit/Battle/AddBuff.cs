using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{
    public class AddBuff : UnitBattleActionBase
    {
        [SerializeField]
        BuffTarget target;
        [SerializeField]
        BuffBase buff;
        public override void OnUpdate(float dt)
        {
            if (target == BuffTarget.Self)
            {
                owner.AddBuff(buff);
            }
            else
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    targets[i].AddBuff(buff);
                }
            }
        }

        private enum BuffTarget { Self, Target }
    }
}