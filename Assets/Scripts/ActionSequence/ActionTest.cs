using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTest : MonoBehaviour
{
    public UnitAbility ability;
    [Button("Execute")]
    void Exec()
    {
        ability.Execute(null,null);
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        ability.OnUpdate(dt);
    }
}
