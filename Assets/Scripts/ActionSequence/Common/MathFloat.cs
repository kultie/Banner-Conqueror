using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class MathFloat : CommonActionBase
    {
        [SerializeField]
        ValueType valueType;
        [ShowIf("@this.actions != null && this.actions.Length > 1")]
        [SerializeField]
        Operator op;

        [ShowIf("valueType", ValueType.RawValue)]
        [SerializeField]
        float value;

        [ShowIf("valueType", ValueType.ActionValue)]
        public UnitActionBase[] actions;

        public override void OnUpdate(float dt)
        {

        }

        public override object GetValue()
        {
            if (valueType == ValueType.RawValue)
            {
                return value;
            }
            float result = (float)actions[0].GetValue();
            for (int i = 1; i < actions.Length; i++)
            {
                float val = (float)actions[i].GetValue();
                switch (op)
                {
                    case Operator.Plus: result += val; break;
                    case Operator.Minus: result -= val; break;
                    case Operator.Mult: result *= val; break;
                    case Operator.Div: result /= val; break;
                    case Operator.Mod: result %= val; break;
                }
            }
            return result;
        }
    }
}