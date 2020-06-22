using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    private void Awake()
    {
        Instance = GetInstance();
    }

    protected abstract T GetInstance();

    private void OnDestroy()
    {
        Instance = null;
    }
}
