using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit
{
    public abstract class UnitActionBase : AbilityActionBase
    {
        public override string DisplayOnEditor()
        {
            return "Unit";
        }    
    }
}