using SimpleJSON;
using System;
using System.Collections.Generic;

public static class ActionDictionary
{
    private static Dictionary<string, CreateCommand> commands = new Dictionary<string, CreateCommand>() {
        { "wait", WaitAction.Create},
        { "wait_stagger", WaitAction.CreateStagger}
    };

    public static CommandAction Get(string key, JSONNode data)
    {
        return commands[key].Invoke(data);
    }
}
