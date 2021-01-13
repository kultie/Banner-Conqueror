
using Sirenix.OdinInspector;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class MathVector : CommonActionBase
    {
        [SerializeField]
        ValueType valueType;
        [ShowIf("@this.actions != null && this.actions.Length > 1")]
        [SerializeField]
        Operator op;

        [ShowIf("valueType", ValueType.RawValue)]
        [SerializeField]
        Vector2 value;

        [ShowIf("valueType", ValueType.ActionValue)]
        [ValueDropdown("TreeView", ExpandAllMenuItems = false)]
        public AbilityActionBase[] actions;

        public override void OnUpdate(float dt)
        {

        }

        public override object GetValue()
        {
            if (valueType == ValueType.RawValue)
            {
                return value;
            }
            Vector2 result = (Vector2)actions[0].GetValue();
            for (int i = 1; i < actions.Length; i++)
            {
                object val = actions[i].GetValue();

                if (val is float)
                {
                    switch (op)
                    {
                        case Operator.Mult: result *= (float)val; break;
                        case Operator.Div: result /= (float)val; break;
                    }
                }
                else if (val is Vector2)
                {
                    switch (op)
                    {
                        case Operator.Plus: result += (Vector2)val; break;
                        case Operator.Minus: result -= (Vector2)val; break;
                        case Operator.Mult: result *= (Vector2)val; break;
                        case Operator.Div: result /= (Vector2)val; break;
                    }

                }
            }
            return result;
        }
    }
}