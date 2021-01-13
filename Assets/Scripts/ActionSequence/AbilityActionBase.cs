using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Linq;

namespace BC.ActionSequence
{
    public enum Operator { Plus, Minus, Mult, Div, Mod }
    public enum ValueType { RawValue, ActionValue }
    public enum CompareOperator { Greater, Lesser, Equal, GreaterOrEqual, LesserOrEqual }
    [System.Serializable]
    public abstract class AbilityActionBase
    {
        protected UnitAbility context;        
        protected UnitEntity owner;
        protected UnitEntity[] targets;

        public virtual string DisplayOnEditor()
        {
            return "Action";
        }

        public virtual void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            owner = entity;
            this.targets = targets;
            this.context = context;
        }

        public abstract void OnUpdate(float dt);
        public virtual object GetValue()
        {
            return null;
        }

        public virtual bool IsFinished()
        {
            return true;
        }

        public virtual bool IsBlock()
        {
            return false;
        }

        //protected IEnumerable TreeView()
        //{
        //    var q = typeof(UnitActionBase).Assembly.GetTypes()
        //        .Where(x => !x.IsAbstract)
        //        .Where(x => !x.IsGenericTypeDefinition)
        //        .Where(x => typeof(UnitActionBase).IsAssignableFrom(x));
        //    return q;
        //}

        public static IEnumerable TreeView()
        {
            ValueDropdownList<AbilityActionBase> result = new ValueDropdownList<AbilityActionBase>();
            var q = typeof(AbilityActionBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(AbilityActionBase).IsAssignableFrom(x));

            foreach (var e in q)
            {
                AbilityActionBase instance = (AbilityActionBase)Activator.CreateInstance(e);
                result.Add(instance.DisplayOnEditor() + "/" + e.Name, (AbilityActionBase)Activator.CreateInstance(e));
            }
            return result;
        }
    }
}