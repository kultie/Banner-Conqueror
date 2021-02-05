using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class Log : CommonActionBase
    {
        [SerializeField]
        ValueType valueType;

        [ShowIf("valueType", ValueType.RawValue)]
        [SerializeField]
        string value;

        [ShowIf("valueType", ValueType.ActionValue)]
        [ValueDropdown("TreeView")]
        [SerializeField]
        AbilityActionBase action;

        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            base.Init(entity, targets, context);
            if (valueType == ValueType.ActionValue)
            {
                action.Init(entity, targets, context);
            }
        }
        protected override void OnUpdate(float dt)
        {
            if (valueType == ValueType.RawValue)
            {
                Debug.Log(value);
            }
            else if (valueType == ValueType.ActionValue)
            {
                Debug.Log(action.GetValue().ToString());
            }
        }
    }
}