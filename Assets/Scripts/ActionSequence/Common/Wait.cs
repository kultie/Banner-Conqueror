using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class Wait : CommonActionBase
    {
        [SerializeField]
        float time;
        float currentTime;

        public override void Init(UnitEntity entity, UnitEntity[] targets)
        {
            currentTime = time;
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