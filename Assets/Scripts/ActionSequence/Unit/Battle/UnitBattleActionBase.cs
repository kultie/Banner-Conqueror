using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{
    public abstract class UnitBattleActionBase : UnitActionBase
    {
        public override string DisplayOnEditor()
        {
            return "Unit/Battle";
        }
    }
}