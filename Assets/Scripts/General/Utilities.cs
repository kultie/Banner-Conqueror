using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Utilities
{
    public static T ConvertToEnum<T>(string str) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), str);
    }

    public static T ToEnum<T>(this string str) where T : Enum
    {
        return ConvertToEnum<T>(str);
    }

    public static T Random<T>(this IEnumerable<T> input)
    {
        Random r = new Random();
        return input.ElementAt(r.Next(input.Count()));
    }
}