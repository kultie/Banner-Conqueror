using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BC.BattleEvent
{
    [Serializable]
    public abstract class BattleEventListener
    {
        public abstract void OnTrigger(Dictionary<string, object> args);
        public virtual string DisplayOnEditor()
        {
            return "BattleEvent";
        }
    }


    public class BattleEventContainer
    {
        [ValueDropdown("TreeView")]
        public BattleEventListener[] events;
        public static IEnumerable TreeView()
        {
            ValueDropdownList<BattleEventListener> result = new ValueDropdownList<BattleEventListener>();
            var q = typeof(BattleEventListener).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(BattleEventListener).IsAssignableFrom(x));

            foreach (var e in q)
            {
                BattleEventListener instance = (BattleEventListener)Activator.CreateInstance(e);
                result.Add(instance.DisplayOnEditor() + "/" + e.Name, (BattleEventListener)Activator.CreateInstance(e));
            }
            return result;
        }
    }
    [Serializable]
    public class BattleEventDictionary : UnitySerializedDictionary<BattleEvents, BattleEventContainer> { }
}
