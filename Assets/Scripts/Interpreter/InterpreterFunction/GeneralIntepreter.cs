using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
public static class GeneralIntepreter
{
    public static Dictionary<string, ParamsFunc> functionMap = new Dictionary<string, ParamsFunc>()
    {
        {"shake_camera", ShakeCamera },
        {"plus_float", PlusFloat },
        {"mul_float", MultiplyFloat },
        {"minus_float", MinusFloat },
        {"div_float", DivideFloat },
        {"check_null", CheckNull },
        {"check_true", CheckTrue },
        {"check_string_empty", CheckStringEmpty },
        {"random", RandomValue },
        {"greater", Greater },
        {"equal", Equal},
        {"lesser", Lesser},
    };

    private static object ShakeCamera(Dictionary<string, object> args)
    {
        float duration = (float)args["duration"];
        float strength = (float)args["strength"];
        int vibrato = (int)args["vibrator"];
        int randomnes = (int)args["randomess"];
        Camera.main.DOShakePosition(duration, new Vector3(strength, strength, 0), vibrato, randomnes);
        return null;
    }
    private static object PlusFloat(Dictionary<string, object> args)
    {
        return (float)args["x"] + (float)args["y"];
    }

    private static object MultiplyFloat(Dictionary<string, object> args)
    {
        return (float)args["x"] * (float)args["y"];
    }

    private static object MinusFloat(Dictionary<string, object> args)
    {
        return (float)args["x"] - (float)args["y"];
    }

    private static object DivideFloat(Dictionary<string, object> args)
    {
        return (float)args["x"] / (float)args["y"];
    }

    private static object Lesser(Dictionary<string, object> args)
    {

        if ((bool)args["check_equal"])
        {
            return (float)args["x"] <= (float)args["y"];
        }
        else
        {
            return (float)args["x"] < (float)args["y"];
        }

    }

    private static object Equal(Dictionary<string, object> args)
    {
        return (float)args["x"] == (float)args["x"];
    }

    private static object Greater(Dictionary<string, object> args)
    {
        bool value = false;
        if ((bool)args["check_equal"])
        {
            value = (float)args["x"] >= (float)args["y"];
        }
        else
        {
            value = (float)args["x"] > (float)args["x"];
        }
        return value;
    }

    private static object CheckStringEmpty(Dictionary<string, object> args)
    {
        return string.IsNullOrEmpty(((string)args["value"]));
    }

    private static object CheckNull(Dictionary<string, object> args)
    {
        return args["value"] != null;
    }

    private static object CheckTrue(Dictionary<string, object> args)
    {
        return (bool)args["value"] == true;
    }
    private static object RandomValue(Dictionary<string, object> args)
    {
        return Random.Range((float)args["min"], (float)args["max"]);
    }
}
