using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Battle
{

    public class DealDamageToTarget : UnitBattleActionBase
    {
        [SerializeField]
        CalculateType calculateType;
        [ShowIf("calculateType", CalculateType.RawValue)]
        [SerializeField]
        float amount;

        [ShowIf("calculateType", CalculateType.Formular)]
        [SerializeField]
        string formular;

        [ShowIf("calculateType", CalculateType.ActionResult)]
        [SerializeField]
        [ValueDropdown("TreeView")]
        AbilityActionBase action;

        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            base.Init(entity, targets, context);
            if (calculateType == CalculateType.ActionResult)
            {
                action.Init(entity, targets, context);
            }
        }
        public override void OnUpdate(float dt)
        {
            float value = 0;
            switch (calculateType)
            {
                case CalculateType.RawValue:
                    value = amount;
                    break;
                case CalculateType.Formular:
                    value = 0;
                    break;
                case CalculateType.ActionResult:
                    value = (float)action.GetValue();
                    break;
            }
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].TakeDamage(value);
            }
        }

        private enum CalculateType { RawValue, Formular, ActionResult };
    }
}