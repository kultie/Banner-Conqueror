using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Display
{
    public class WaitForAnimation : UnitDisplayActionBase
    {
        public override void OnUpdate(float dt)
        {

        }

        public override bool IsFinished()
        {
            return owner.display.AnimFinished();
        }
    }
}
