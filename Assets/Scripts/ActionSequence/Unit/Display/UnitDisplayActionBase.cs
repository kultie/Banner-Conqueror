using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Display
{
    public abstract class UnitDisplayActionBase : UnitActionBase
    {
        public override string DisplayOnEditor()
        {
            return "Unit/Display";
        }
    }
}