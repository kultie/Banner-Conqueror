using BC.ActionSequence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Animation
{
    public abstract class AnimationAction : AbilityActionBase
    {
        [SerializeField]
        protected bool waitForAnimation = true;

        public override string DisplayOnEditor()
        {
            return "Animation";
        }

        public override bool IsBlock()
        {
            return waitForAnimation;
        }

        public override bool IsFinished()
        {
            if (waitForAnimation)
                return owner.display.AnimFinished();
            else return true;
        }
    }
}