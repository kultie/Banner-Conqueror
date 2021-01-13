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
    public abstract class UnitActionBase
    {
        [HideInEditorMode]
        public UnitEntity owner;

        [HideInEditorMode]
        public UnitEntity[] targets;

        public virtual string DisplayOnEditor()
        {
            return "Action";
        }

        public virtual void Init(UnitEntity entity, UnitEntity[] targets)
        {
            this.owner = entity;
            this.targets = targets;
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
            ValueDropdownList<UnitActionBase> result = new ValueDropdownList<UnitActionBase>();
            var q = typeof(UnitActionBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(UnitActionBase).IsAssignableFrom(x));

            foreach (var e in q)
            {
                UnitActionBase instance = (UnitActionBase)Activator.CreateInstance(e);
                result.Add(instance.DisplayOnEditor() + "/" + e.Name, (UnitActionBase)Activator.CreateInstance(e));
            }
            return result;
        }
    }
}