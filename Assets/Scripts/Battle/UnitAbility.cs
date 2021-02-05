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
    public Sprite icon;
    public int cooldown;
    public TargetType targetType;

    private int currentCoolDown;

    [ValueDropdown("TreeView")]
    public List<AbilityActionBase> actions;
    List<AbilityActionBase> runningActions;
    [SerializeField]
    int startedCooldown;

    protected IEnumerable TreeView()
    {
        return AbilityActionBase.TreeView();
    }

    public void Execute(UnitEntity owner, UnitEntity[] targets)
    {
        currentCoolDown = cooldown;
        if (actions != null)
        {
            runningActions = new List<AbilityActionBase>(actions);
            runningActions.ForEach(a => a.Init(owner, targets, this));
        }
    }

    public void ProcessCooldown()
    {
        currentCoolDown--;
    }

    public bool IsCooledDown()
    {
        return currentCoolDown <= 0;
    }

    public int CurrentCoolDown()
    {
        return currentCoolDown;
    }

    public void OnUpdate(float dt)
    {
        if (runningActions != null && runningActions.Count > 0)
        {
            int deleteIndex = -1;
            for (int i = 0; i < runningActions.Count; i++)
            {
                AbilityActionBase act = runningActions[i];
                act.PublicUpdate(dt);
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
        if (runningActions == null) return true;
        return runningActions.Count == 0;
    }

    public UnitAbility Clone()
    {
        return Instantiate(this);
    }
}
