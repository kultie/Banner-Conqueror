using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Animation
{
    public class WaitForAnimation : AnimationAction
    {
        protected override void OnUpdate(float dt)
        {

        }

        public override bool IsFinished()
        {
            return owner.display.AnimFinished();
        }
    }
}
