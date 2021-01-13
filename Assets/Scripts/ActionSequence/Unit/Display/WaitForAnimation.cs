using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Display
{
    public class WaitForAnimation : UnitDisplayActionBase
    {
        protected override bool ShowTargetSelf()
        {
            return false;
        }

        public override void OnUpdate(float dt)
        {

        }

        public override bool IsFinished()
        {
            return owner.display.AnimFinished();
        }
    }
}
