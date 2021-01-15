using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDisplay : MonoBehaviour, IPointerClickHandler
{
    public UnitEntity unitModel;
    private UnitData unitData;
    public SpriteRenderer unitAvatar;
    public SpriteRenderer unitBanner;
    private AnimationSystem anim;
    bool pauseAnimation;
    public string currentAnimID { private set; get; }
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
        if (anim != null && !pauseAnimation)
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
        currentAnimID = id;
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

    public void RequestAnimation(AnimationData animData)
    {
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

    public void SetSprite(int spriteIndex)
    {
        unitAvatar.sprite = unitModel.data.GetSprite(spriteIndex);
    }

    public void SetSpriteAlpha(float value)
    {
        Color col = unitAvatar.color;
        col.a = value;
        unitAvatar.color = col;
    }

    public bool AnimFinished()
    {
        if (anim == null)
        {
            return true;
        }
        if (anim.IsLoop())
        {
            return true;
        }
        return anim.IsFinished();
    }

    public void PauseAnimation()
    {
        pauseAnimation = true;
    }

    public void ResumeAnimation()
    {
        pauseAnimation = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BattleController.Instance.SetPlayerCurrentTarget(unitModel);
    }
}
