using B83.ExpressionParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameFormula
{
    private static ExpressionParser parser = new ExpressionParser();
    public static float GetValue(string data, UnitEntity attacker, UnitEntity defender)
    {

        Expression exp = parser.EvaluateExpression(data);
        foreach (var a in exp.Parameters)
        {
            exp.Parameters[a.Key].Value = GetValueFromEntity(a.Key, attacker, defender);
        }
        return (float)exp.Value;
    }

    private static float GetValueFromEntity(string data, UnitEntity attacker, UnitEntity defender)
    {
        string[] _args = data.Split('.');
        if (_args[0] == "a") return attacker.GetStats(_args[1]);
        if (_args[0] == "b") return defender.GetStats(_args[1]);
        return 0;
    }
}
