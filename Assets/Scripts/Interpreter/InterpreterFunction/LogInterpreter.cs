using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogInterpreter
{
    public static Dictionary<string, ParamsFunc> functionMap = new Dictionary<string, ParamsFunc>()
    {
        {"string", LogString },
        {"object", LogObject },
        {"vector", LogVector2 },
        {"float", LogFloat },
        {"bool", LogBool }
    };

    private static object LogBool(Dictionary<string, object> args)
    {
        Debug.Log((bool)args["value"]);
        return null;
    }

    private static object LogString(Dictionary<string, object> args)
    {
        Debug.Log((string)args["value"]);
        return null;
    }

    private static object LogObject(Dictionary<string, object> args)
    {
        Debug.Log(args["value"].ToString());
        return null;
    }

    private static object LogVector2(Dictionary<string, object> args)
    {
        Debug.Log((Vector2)args["value"]);
        return null;
    }

    private static object LogFloat(Dictionary<string, object> args)
    {
        Debug.Log((float)args["value"]);
        return null;
    }
}
