using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public string id;
    public Vector2 position;
    public Dictionary<string, object> variables = new Dictionary<string, object>();

    protected void Remove()
    {

    }

    protected void Create(string prefix)
    {
        id = prefix + System.Guid.NewGuid().ToString();
        variables["current_time"] = 0f;
    }

    public void Update(float dt)
    {
        variables["delta_time"] = dt;
        OnUpdate(dt);
    }

    protected abstract void OnUpdate(float dt);
    public void ForceDestroy()
    {
        Remove();
    }

    protected abstract void OnForceDestroy();
}
