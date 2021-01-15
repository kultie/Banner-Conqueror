using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FXEntityDisplay<T> : MonoBehaviour where T : FXEntity
{
    protected T entity;

    public void SetEntity(T entity)
    {
        this.entity = entity;
        Initialized(entity);
    }

    protected virtual void Initialized(T entity) { }
    public void OnEnable()
    {
        BattleController.Instance.updateEntityAnimation += OnUpdate;
    }


    public void OnDisable()
    {
        BattleController.Instance.updateEntityAnimation -= OnUpdate;
        InternalOnDisable();
    }
    protected virtual void InternalOnDisable()
    {

    }

    protected abstract void OnUpdate(float dt);
}
