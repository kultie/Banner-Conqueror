using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Battle
{
    public abstract class UnitBattleActionBase : AbilityActionBase
    {
        public override string DisplayOnEditor()
        {
            return "Battle";
        }
    }
}