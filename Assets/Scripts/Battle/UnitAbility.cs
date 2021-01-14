using BC.ActionSequence;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "BC/Create Ability")]
public class UnitAbility : SerializedScriptableObject
{
    public TargetType targetType;

    [ValueDropdown("TreeView")]
    public List<AbilityActionBase> actions;
    List<AbilityActionBase> runningActions;

    protected IEnumerable TreeView()
    {
        return AbilityActionBase.TreeView();
    }

    public void Execute(UnitEntity owner, UnitEntity[] targets)
    {
        runningActions = new List<AbilityActionBase>(actions);
        runningActions.ForEach(a => a.Init(owner, targets, this));
    }

    public void OnUpdate(float dt)
    {
        if (runningActions != null && runningActions.Count > 0)
        {
            int deleteIndex = -1;
            for (int i = 0; i < runningActions.Count; i++)
            {
                AbilityActionBase act = runningActions[i];
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

    public bool IsFinished()
    {
        return runningActions.Count == 0;
    }
}
