using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class WaitStagger : CommonActionBase
    {
        float currentTime;

        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            this.context = context;
            currentTime = GameConfig.STAGGER_TIME + 0.05f;
        }
        public override void OnUpdate(float dt)
        {
            currentTime -= dt;
        }
        public override bool IsFinished()
        {
            return currentTime <= 0;
        }

        public override bool IsBlock()
        {
            return true;
        }
    }
}