using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit
{
    public abstract class UnitActionBase : AbilityActionBase
    {
        [ShowIf("@this.ShowTargetSelf()")]
        [SerializeField]
        protected bool targetSelf;
        public override string DisplayOnEditor()
        {
            return "Action/Unit";
        }

        protected virtual bool ShowTargetSelf()
        {
            return true;
        }

        protected virtual void ResolvingTarget(Action<UnitEntity> actionToResolve)
        {
            if (targetSelf)
            {
                actionToResolve(owner);
            }
            else
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    actionToResolve(targets[i]);
                }
            }
        }
    }
}