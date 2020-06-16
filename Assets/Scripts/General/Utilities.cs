using System;

public static class Utilities
{
    public static T ConvertToEnum<T>(string str) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), str);
    }
}