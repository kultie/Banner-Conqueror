using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate JSONNode JSONFunc(JSONNode args);
public static class CommandInterpreter
{
    public static Dictionary<string, JSONFunc> functionMap = new Dictionary<string, JSONFunc>() {
        {"wait", WaitCommand }
    };

    private static JSONNode WaitCommand(JSONNode args)
    {
        float timeWait = args["time"].AsFloat;
        string json = "{\"on_execute\":[{\"type\":\"function\",\"namespace\":\"entity\",\"command\":\"save_variable\",\"args\":{\"variable_id\":{\"type\":\"value\",\"value_type\":\"string\",\"value\":\"time\"},\"variable_value\":{\"type\":\"value\",\"value_type\":\"float\",\"value\":" + timeWait + "}}}],\"on_update\":[{\"type\":\"function\",\"namespace\":\"entity\",\"command\":\"save_variable\",\"args\":{\"variable_id\":{\"type\":\"value\",\"value_type\":\"string\",\"value\":\"time\"},\"variable_value\":{\"type\":\"function\",\"namespace\":\"general\",\"command\":\"minus_float\",\"args\":{\"x\":{\"type\":\"function\",\"namespace\":\"entity\",\"command\":\"get_variable\",\"args\":{\"variable_id\":{\"type\":\"value\",\"value_type\":\"string\",\"value\":\"time\"}}},\"y\":{\"type\":\"function\",\"namespace\":\"entity\",\"command\":\"get_variable\",\"args\":{\"variable_id\":{\"type\":\"value\",\"value_type\":\"string\",\"value\":\"delta_time\"}}}}}}}],\"is_finish\":{\"type\":\"function\",\"namespace\":\"general\",\"command\":\"lesser\",\"args\":{\"check_equal\":{\"type\":\"value\",\"value_type\":\"bool\",\"value\":true},\"x\":{\"type\":\"function\",\"namespace\":\"entity\",\"command\":\"get_variable\",\"args\":{\"variable_id\":{\"type\":\"value\",\"value_type\":\"string\",\"value\":\"time\"}}},\"y\":{\"type\":\"value\",\"value_type\":\"float\",\"value\":0}}},\"is_block\":{\"type\":\"value\",\"value_type\":\"bool\",\"value\":true}}";
        return JSON.Parse(json);
    }

    public static JSONNode ConvertCommand(JSONNode command)
    {
        JSONNode data = command;
        if (command["type"] != null && command["type"].Value == "predefined")
        {
            data = functionMap[data["name"].Value]?.Invoke(data["args"]);
        }
        return data;
    }
}
