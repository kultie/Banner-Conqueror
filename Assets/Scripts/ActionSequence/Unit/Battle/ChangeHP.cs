using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{

    public class ChangeHP : UnitBattleActionBase
    {
        protected enum CalculateType { RawValue, Formular, ActionResult };
        [SerializeField]
        protected CalculateType calculateType;
        [ShowIf("calculateType", CalculateType.RawValue)]
        [SerializeField]
        protected float amount;

        [ShowIf("calculateType", CalculateType.Formular)]
        [SerializeField]
        protected string formular;

        [ShowIf("calculateType", CalculateType.ActionResult)]
        [SerializeField]
        [ValueDropdown("TreeView")]
        protected AbilityActionBase action;

        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            base.Init(entity, targets, context);
            if (calculateType == CalculateType.ActionResult)
            {
                action.Init(entity, targets, context);
            }
        }

        protected float GetValue(UnitEntity target)
        {
            float value = 0;
            switch (calculateType)
            {
                case CalculateType.RawValue:
                    value = amount;
                    break;
                case CalculateType.Formular:
                    value = GameFormula.GetValue(formular, owner, target);
                    break;
                case CalculateType.ActionResult:
                    value = (float)action.GetValue();
                    break;
            }
            return value;
        }

        public override void OnUpdate(float dt)
        {
            owner.ChangeHP(GetValue(owner));
        }
    }
}