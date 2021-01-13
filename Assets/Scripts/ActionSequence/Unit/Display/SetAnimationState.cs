using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Display
{
    public class SetAnimationState : UnitDisplayActionBase
    {
        [SerializeField]
        bool resume;
        public override void OnUpdate(float dt)
        {
            if (!resume)
            {
                owner.display.PauseAnimation();
            }
            else
            {
                owner.display.ResumeAnimation();
            }
        }
    }
}