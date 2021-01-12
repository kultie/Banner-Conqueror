using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Kultie.Stats
{
    public class Stats<T> where T : Enum
    {
        protected Dictionary<T, float> baseStats = new Dictionary<T, float>();

        protected Dictionary<object, string> statsModifiersSources = new Dictionary<object, string>();
        protected Dictionary<string, JSONNode> statsModifiers = new Dictionary<string, JSONNode>();

        private Dictionary<T, bool> dirtyStats = new Dictionary<T, bool>();
        protected Dictionary<T, float> currentStats = new Dictionary<T, float>();

        public Stats(JSONNode def)
        {
            foreach (KeyValuePair<string, JSONNode> s in def.AsObject)
            {
                T statsType = Utilities.ConvertToEnum<T>(s.Key);
                baseStats[statsType] = s.Value.AsFloat;
                dirtyStats[statsType] = false;
            }
        }

        public void AddModifier(string id, JSONNode modifier)
        {
            statsModifiers[id] = modifier;
            SetDirtyAfterSetModifier(modifier);
        }

        public void AddModifier(string id, JSONNode modifier, object source)
        {
            AddModifier(id, modifier);
            statsModifiersSources[source] = id;
        }

        public void RemoveModifier(string id)
        {
            var modToRemove = statsModifiers[id];
            SetDirtyAfterSetModifier(modToRemove);
            statsModifiers.Remove(id);
        }

        public void RemoveModifier(object source)
        {
            RemoveModifier(statsModifiersSources[source]);
            statsModifiersSources.Remove(source);
        }

        public void ClearModifier()
        {
            statsModifiers.Clear();
        }

        public float GetStats(T key)
        {
            if (dirtyStats[key])
            {
                float baseValue = baseStats[key];
                float flatValue = 0;
                float multValue = 0;
                var mods = statsModifiers.Values.ToArray();
                foreach (var data in mods)
                {
                    flatValue += data["flat"][key.ToString()].AsFloat;
                    multValue += data["mult"][key.ToString()].AsFloat;
                }
                float realValue = (baseValue + flatValue) * (1 + multValue);
                currentStats[key] = realValue;
                dirtyStats[key] = false;
            }
            return currentStats[key];

        }

        private void SetDirtyAfterSetModifier(JSONNode modifier)
        {
            JSONNode flatData = modifier["flat"];
            JSONNode multData = modifier["mult"];

            SetDirty(flatData);
            SetDirty(multData);
        }

        private void SetDirty(JSONNode modData)
        {
            foreach (KeyValuePair<string, JSONNode> data in modData.AsObject)
            {
                T statsType = Utilities.ConvertToEnum<T>(data.Key);
                dirtyStats[statsType] = true;
            }
        }

        public float GetCurrentStats(T key)
        {
            return currentStats[key];
        }
    }
}