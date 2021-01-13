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
            ResolvingTarget(e =>
            {
                if (!resume)
                {
                    e.display.PauseAnimation();
                }
                else
                {
                    e.display.ResumeAnimation();
                }
            });
        }
    }
}