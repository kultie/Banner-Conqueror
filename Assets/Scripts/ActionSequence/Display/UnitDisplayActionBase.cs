using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Display
{
    public abstract class DisplayAction : AbilityActionBase
    {
        public override string DisplayOnEditor()
        {
            return "Display";
        }
    }
}