using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.CameraSequence
{
    public class WaitAllAction : AbilityActionBase
    {
        public override string DisplayOnEditor()
        {
            return "Camera";
        }
        protected override void OnUpdate(float dt)
        {

        }

        public override bool IsFinished()
        {
            return CameraAction.AllTweenFinished();
        }

        public override bool IsBlock()
        {
            return true;
        }
    }
}