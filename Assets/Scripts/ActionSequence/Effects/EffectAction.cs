using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Effect
{
    public class EffectAction : AbilityActionBase
    {
        [SerializeField]
        protected bool waitForEffect;
        public override string DisplayOnEditor()
        {
            return "Effects";
        }

        public override bool IsBlock()
        {
            return waitForEffect;
        }
    }
}