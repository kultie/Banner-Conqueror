using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityInterpreter : MonoBehaviour
{
    public static Dictionary<string, ParamsFunc> functionMap = new Dictionary<string, ParamsFunc>()
    {
        {"id_from_entity", GetIDFromEntity},
        {"get_entity_from_id", GetEntityFromID },
        {"save_variable", SaveVariable},
        {"get_variable", GetVariable },
        {"get_position", GetPosition },
        {"destroy", DestroyEntity }
    };

    private static object GetIDFromEntity(Dictionary<string, object> args)
    {
        try
        {
            Entity entity = (Entity)args["entity"];
            return entity.id;
        }
        catch (Exception)
        {
            Debug.LogError("Arguments is not entity type");
            return null;
        }
    }

    private static object GetEntityFromID(Dictionary<string, object> args)
    {
        return GameManager.Instance.allEntities[(string)args["entity_id"]];
    }

    private static object SaveVariable(Dictionary<string, object> args)
    {
        Entity entity = args["entity"] as Entity;
        if (entity != null)
        {
            entity.variables[(string)args["variable_id"]] = args["variable_value"];
        }
        else
        {
            Debug.Log("Error getting entity from arguments for function" + "save_variable");
        }
        return null;
    }

    private static object GetVariable(Dictionary<string, object> args)
    {
        Entity entity = args["entity"] as Entity;
        string id = (string)args["variable_id"];
        if (entity != null)
        {
            object result = null;
            if (entity.variables.TryGetValue(id, out result))
            {
                return result;
            }
            else
            {
                Debug.Log("Cannot find variable with id: " + id + " in entity: ");
                return null;
            }
        }
        else
        {
            Debug.Log("Error getting entity from arguments for function" + "get_variable");
            return null;
        }
    }

    private static object GetPosition(Dictionary<string, object> args)
    {
        try
        {
            Entity entity = (Entity)args["entity"];
            return entity.position;
        }
        catch (Exception)
        {
            Debug.LogError("Arguments is not entity type");
            return null;
        }
    }

    private static object DestroyEntity(Dictionary<string, object> args)
    {
        Entity e = (Entity)args["entity"];
        e.ForceDestroy();
        return null;
    }
}
