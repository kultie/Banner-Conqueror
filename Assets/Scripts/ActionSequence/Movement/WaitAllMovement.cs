using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Movement
{
    public class WaitAllMovement : AbilityActionBase
    {
        public override string DisplayOnEditor()
        {
            return "Movement/";
        }
        protected override void OnUpdate(float dt)
        {

        }

        public override bool IsFinished()
        {
            return MovementAction.AllTweenFinished();
        }

        public override bool IsBlock()
        {
            return true;
        }
    }
}