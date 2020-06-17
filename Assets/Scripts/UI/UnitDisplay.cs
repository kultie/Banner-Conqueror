using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDisplay : MonoBehaviour
{
    public UnitEntity unitModel;
    private UnitData unitData;
    public SpriteRenderer unitAvatar;
    public SpriteRenderer unitBanner;
    private AnimationSystem anim;
    public void SetUp(UnitEntity unit, TeamSide teamSide)
    {
        this.unitData = unit.data;
        SetSprite(unitData.sprites[0]);
        switch (teamSide)
        {
            case TeamSide.Player:
                unitModel = unit;
                break;
            case TeamSide.Enemy:
                unitModel = unit;
                break;
        }
        unitModel.SetDisplay(this);
    }

    private void OnEnable()
    {
        BattleController.Instance.updateEntityAnimation += UpdateAnimation;
        BattleController.Instance.updateEntity += UpdateUnit;
    }

    private void OnDisable()
    {
        BattleController.Instance.updateEntityAnimation -= UpdateAnimation;
        BattleController.Instance.updateEntity -= UpdateUnit;
    }



    public void UpdateAnimation(float dt)
    {
        if (anim != null)
        {
            anim.Update(dt);
            SetSprite(anim.Frame());
        }
    }

    public void UpdateUnit(float dt)
    {
        if (unitModel != null)
        {
            unitModel.Update(dt);
        }
    }

    public void RequestAnimation(string id)
    {
        AnimationData animData = unitData.GetAnimation(id);
        if (anim == null)
        {
            anim = new AnimationSystem(animData.frames, animData.loop, animData.spf);
        }
        else
        {
            anim.SetFrames(animData.frames, animData.loop, animData.spf);
        }
    }

    public void SetSprite(Sprite sprite)
    {
        unitAvatar.sprite = sprite;
    }

    public bool AnimFinished()
    {
        if (anim == null)
        {
            return true;
        }
        return anim.IsFinished();
    }
}
