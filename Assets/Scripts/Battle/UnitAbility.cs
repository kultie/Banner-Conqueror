using BC.ActionSequence;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Create Ability", order = -1)]
public class UnitAbility : SerializedScriptableObject
{
    [ValueDropdown("TreeView")]
    public List<UnitActionBase> actions;
    List<UnitActionBase> runningActions;
    protected IEnumerable TreeView()
    {
        return UnitActionBase.TreeView();
    }

    public void Execute(UnitEntity owner, UnitEntity[] targets)
    {
        runningActions = new List<UnitActionBase>(actions);
        runningActions.ForEach(a => a.Init(owner, targets));
    }

    public void OnUpdate(float dt)
    {
        if (runningActions != null && runningActions.Count > 0)
        {
            int deleteIndex = -1;
            for (int i = 0; i < runningActions.Count; i++)
            {
                UnitActionBase act = runningActions[i];
                act.OnUpdate(dt);
                if (act.IsFinished())
                {
                    deleteIndex = i;
                    break;
                }
                if (act.IsBlock())
                {
                    break;
                }
            }
            if (deleteIndex != -1)
            {
                runningActions.RemoveAt(deleteIndex);
            }
        }
    }
}
