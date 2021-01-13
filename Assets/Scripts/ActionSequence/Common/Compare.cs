
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class Compare : CommonActionBase
    {
        [SerializeField]
        CompareOperator compareOp;
        [Header("First number")]
        [SerializeField]
        ValueType aValueType;
        [ShowIf("aValueType", ValueType.RawValue)]
        [SerializeField]
        float a;
        [ShowIf("aValueType", ValueType.ActionValue)]
        [ValueDropdown("TreeView")]
        [SerializeField]
        AbilityActionBase aValue;
        [Header("Second number")]
        [SerializeField]
        ValueType bValueType;

        [ShowIf("bValueType", ValueType.RawValue)]
        [SerializeField]
        float b;

        [ShowIf("bValueType", ValueType.ActionValue)]
        [ValueDropdown("TreeView")]
        [SerializeField]
        AbilityActionBase bValue;

        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            base.Init(entity, targets, context);
            if (aValueType == ValueType.ActionValue)
            {
                aValue.Init(entity, targets, context);
            }
            if (bValueType == ValueType.ActionValue)
            {
                bValue.Init(entity, targets, context);
            }
        }

        public override void OnUpdate(float dt)
        { }

        public override object GetValue()
        {
            float _a = aValueType == ValueType.RawValue ? a : (float)aValue.GetValue();
            float _b = bValueType == ValueType.RawValue ? b : (float)bValue.GetValue();
            switch (compareOp)
            {
                case CompareOperator.Greater:
                    return _a > _b;
                case CompareOperator.Lesser:
                    return _a < _b;
                case CompareOperator.Equal:
                    return _a == _b;
                case CompareOperator.GreaterOrEqual:
                    return _a >= _b;
                case CompareOperator.LesserOrEqual:
                    return _a <= _b;
                default: return false;
            }
        }
    }
}