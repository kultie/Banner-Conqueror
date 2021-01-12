using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie
{
    public static class Utilities
    {
        public static T ConvertToEnum<T>(string str) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), str);
        }
    }
}