using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Animation
{
    public class SetAnimationState : AnimationAction
    {
        [SerializeField]
        bool resume;
        protected override void OnUpdate(float dt)
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

        public override bool IsBlock()
        {
            return false;
        }
    }
}