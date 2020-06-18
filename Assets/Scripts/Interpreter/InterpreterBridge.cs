using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
public delegate object ParamsFunc(Dictionary<string, object> args);
public static class InterpreterBridge
{
    static bool DebugMode = true;
    static bool FallbackDebug = false;

    const string functionType = "function";
    const string conditionType = "condition";
    const string delayType = "delay";
    const string valueType = "value";
    const string loopType = "loop";

    public static void RunCommands(JSONArray commands, Dictionary<string, object> context)
    {
        for (int i = 0; i < commands.Count; ++i)
        {
            JSONNode behavior = commands[i];
            string behaviorType = behavior["type"].Value;
            switch (behaviorType)
            {
                case functionType:
                    ParamsFunc func = GetFunction(behavior);
                    func?.Invoke(GeneratingArguments(behavior["args"], context));
                    break;
                case conditionType:
                    ConditionTypeResolve(behavior, context);
                    break;
                case delayType:
                    DelayTypeResolve(behavior, context);
                    break;
                case loopType:
                    LoopTypeResolve(behavior, context);
                    break;

            }
        }
    }

    public static object RunCommand(JSONNode command, Dictionary<string, object> context)
    {
        string commandType = command["type"].Value;
        switch (commandType)
        {
            case functionType:
                ParamsFunc func = GetFunction(command);
                return func?.Invoke(GeneratingArguments(command["args"], context));
            case valueType:
                switch (command["value_type"])
                {
                    case "string":
                        return command[valueType].Value;
                    case "float":
                        return command[valueType].AsFloat;
                    case "int":
                        return command[valueType].AsInt;
                    case "bool":
                        return command[valueType].AsBool;
                }
                break;

        }
        return null;
    }

    private static ParamsFunc GetFunction(JSONNode behavior)
    {
        if (DebugMode)
        {
            if (behavior["__COMMENT__"] != null)
            {
                UnityEngine.Debug.Log(behavior["__COMMENT__"].Value);
            }
            else
            {
                if (FallbackDebug)
                {
                    UnityEngine.Debug.Log("Running: " + behavior["namespace"].Value + ": " + behavior["command"].Value);
                }
            }
        }
        string cmd = behavior["command"].Value;
        switch (behavior["namespace"])
        {
            case "log":
                return LogInterpreter.functionMap[cmd];
            case "entity":
                return EntityInterpreter.functionMap[cmd];
            case "unit":
                return UnitInterpreter.functionMap[cmd];
            case "general":
                return GeneralIntepreter.functionMap[cmd];
            //case "ship":
            //    return ShipInterpreter.functionMap[cmd];
            //case "bullet":
            //    return BulletInterpreter.functionMap[cmd];
            default:
                return null;
        }
    }

    private static Dictionary<string, object> GeneratingArguments(JSONNode args, Dictionary<string, object> context)
    {

        Dictionary<string, object> results = new Dictionary<string, object>();

        foreach (KeyValuePair<string, object> input in context)
        {
            results[input.Key] = input.Value;
        }

        foreach (KeyValuePair<string, JSONNode> input in args.AsObject)
        {
            string key = input.Key;
            JSONNode arg = input.Value;
            switch (arg["type"])
            {
                case valueType:
                    switch (arg["value_type"])
                    {
                        case "string":
                            results[key] = arg[valueType].Value;
                            break;
                        case "float":
                            results[key] = arg[valueType].AsFloat;
                            break;
                        case "int":
                            results[key] = arg[valueType].AsInt;
                            break;
                        case "bool":
                            results[key] = arg[valueType].AsBool;
                            break;
                    }
                    break;
                case functionType:
                    ParamsFunc func = GetFunction(arg);
                    Dictionary<string, object> _args = GeneratingArguments(arg["args"], context);
                    results[key] = func?.Invoke(_args);
                    break;
            }
        }
        return results;
    }

    static void ConditionTypeResolve(JSONNode command, Dictionary<string, object> context)
    {
        ParamsFunc func = GetFunction(command);
        Dictionary<string, object> _args = GeneratingArguments(command["args"], context);
        bool isTrue = (bool)func.Invoke(_args);
        JSONArray commands = null;
        if (isTrue)
        {
            commands = command["when_true"]?.AsArray;
        }
        else
        {
            commands = command["when_false"]?.AsArray;
        }
        if (commands != null)
        {
            RunCommands(commands, context);
        }
    }

    static void DelayTypeResolve(JSONNode args, Dictionary<string, object> context)
    {
        float delayTime = 0;
        JSONNode delayTimeData = args["delay_time"];
        JSONNode delayLoopData = args["loop"];
        JSONNode delayLoopTimeData = args["loop_time"];
        JSONArray methods = args["method"].AsArray;
        string tag = string.Empty;
        bool loop = false;
        int loopTime = 0;
        switch (delayTimeData["type"].Value)
        {
            case functionType:
                ParamsFunc func = GetFunction(delayTimeData);
                Dictionary<string, object> _args = GeneratingArguments(delayTimeData["args"], context);
                delayTime = (float)func?.Invoke(_args);
                break;
            case valueType:
                delayTime = delayTimeData[valueType].AsFloat;
                break;
        }

        if (delayLoopData != null)
        {
            switch (delayLoopData["type"].Value)
            {
                case functionType:
                    ParamsFunc func = GetFunction(delayLoopData);
                    Dictionary<string, object> _args = GeneratingArguments(delayLoopData["args"], context);
                    loop = (bool)func?.Invoke(_args);
                    break;
                case valueType:
                    loop = delayLoopData[valueType].AsBool;
                    break;
            }
        }

        if (delayLoopTimeData != null)
        {
            switch (delayLoopTimeData["type"].Value)
            {
                case functionType:
                    ParamsFunc func = GetFunction(delayLoopTimeData);
                    Dictionary<string, object> _args = GeneratingArguments(delayLoopTimeData["args"], context);
                    loopTime = (int)func?.Invoke(_args);
                    break;
                case valueType:
                    loopTime = delayLoopTimeData[valueType].AsInt;
                    break;
            }
        }

        if (loopTime == 0)
        {
            GameManager.Instance.timer.After(delayTime, () =>
            {
                RunCommands(methods, context);
            }, loop, tag);
        }
        else
        {
            GameManager.Instance.timer.After(delayTime, () =>
            {
                RunCommands(methods, context);
            }, loopTime, tag);
        }
    }

    private static void LoopTypeResolve(JSONNode args, Dictionary<string, object> context)
    {
        JSONArray methods = args["method"].AsArray;
        int currentRun = 0;
        int runLimit = 1000;
        if (args["run_limit"] != null)
        {
            runLimit = args["run_limit"].AsInt;
        }
        JSONNode breakMethod = args["break_method"];
        ParamsFunc func = GetFunction(breakMethod);
        Dictionary<string, object> _args = GeneratingArguments(breakMethod["args"], context);
        while (true)
        {
            currentRun++;
            if (currentRun > runLimit)
            {
                break;
            }
            RunCommands(methods, context);
            if (func == null)
            {
                continue;
            }
            else if ((bool)func.Invoke(_args))
            {
                break;
            }

        }

    }
}