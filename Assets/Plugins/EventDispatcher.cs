using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.EventDispatcher
{
    public static class EventDispatcher
    {
        private static Dictionary<string, Action<Dictionary<string, object>>> listeners = new Dictionary<string, Action<Dictionary<string, object>>>();

        public static void RegisterEvent(string key, Action<Dictionary<string, object>> function)
        {
            if (!listeners.TryGetValue(key, out Action<Dictionary<string, object>> tmp))
            {
                listeners[key] = null;
            }
            listeners[key] += function;
        }

        public static void CallEvent(string key, Dictionary<string, object> arg)
        {
            if (listeners.TryGetValue(key, out Action<Dictionary<string, object>> func))
            {

                func?.Invoke(arg);
            }

        }

        public static void UnRegisterEvent(string key, Action<Dictionary<string, object>> function)
        {
            if (listeners.TryGetValue(key, out Action<Dictionary<string, object>> tmp))
            {
                tmp -= function;
            }
        }

        public static void ClearAllListener()
        {
            listeners.Clear();
        }
    }
}