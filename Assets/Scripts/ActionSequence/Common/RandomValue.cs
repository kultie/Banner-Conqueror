using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class RandomValue : CommonActionBase
    {
        [SerializeField]
        ValueType minValueType;
        [ShowIf("minValueType", ValueType.RawValue)]
        [SerializeField]
        float min;
        [ShowIf("minValueType", ValueType.ActionValue)]
        [ValueDropdown("TreeView")]
        [SerializeField]
        AbilityActionBase minValue;

        [SerializeField]
        ValueType maxValueType;

        [ShowIf("maxValueType", ValueType.RawValue)]
        [SerializeField]
        float max;

        [ShowIf("maxValueType", ValueType.ActionValue)]
        [ValueDropdown("TreeView")]
        [SerializeField]
        AbilityActionBase maxValue;

        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            base.Init(entity, targets, context);
            if (minValueType == ValueType.ActionValue) {
                minValue.Init(entity, targets, context);
            }
            if (maxValueType == ValueType.ActionValue)
            {
                maxValue.Init(entity, targets, context);
            }
        }
        protected override void OnUpdate(float dt)
        {
        }

        public override object GetValue()
        {
            float _min = minValueType == ValueType.RawValue ? min : (float)minValue.GetValue();
            float _max = maxValueType == ValueType.RawValue ? max : (float)maxValue.GetValue();
            return UnityEngine.Random.Range(_min, _max);
        }
    }
}