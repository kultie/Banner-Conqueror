using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class Condition : CommonActionBase
    {
        [SerializeField]
        bool isBlock;
        [SerializeField]
        [ValueDropdown("TreeView")]
        AbilityActionBase condition;

        [SerializeField]
        [ValueDropdown("TreeView")]
        List<AbilityActionBase> whenTrue;

        [SerializeField]
        [ValueDropdown("TreeView")]
        List<AbilityActionBase> whenFalse;
        List<AbilityActionBase> actions;

        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            base.Init(entity, targets, context);
            condition.Init(entity, targets, context);
            whenTrue.ForEach(a => a.Init(entity, targets, context));
            whenFalse.ForEach(a => a.Init(entity, targets, context));
        }
        public override void OnUpdate(float dt)
        {
            if (actions == null || actions.Count == 0)
            {
                bool condition = (bool)this.condition.GetValue();
                actions = new List<AbilityActionBase>(condition ? whenTrue : whenFalse);
            }

            int deleteIndex = -1;
            for (int i = 0; i < actions.Count; i++)
            {
                AbilityActionBase act = actions[i];
                act.OnUpdate(dt);
                if (act.IsFinished())
                {
                    deleteIndex = i;
                    break;
                }
                if (act.IsBlock())
                {
                    break;
                }
            }
            if (deleteIndex != -1)
            {
                actions.RemoveAt(deleteIndex);
            }
        }

        public override bool IsBlock()
        {
            return isBlock;
        }

        public override bool IsFinished()
        {
            return actions.Count == 0;
        }
    }
}