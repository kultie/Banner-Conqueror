using DG.Tweening;
using Kultie.EventDispatcher;
using SimpleJSON;
using System;
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
    Vector2 homePosition;
    bool pauseAnimation;
    public string currentAnimID { private set; get; }
    public void SetUp(UnitEntity unit, TeamSide teamSide)
    {
        unitData = unit.data;
        homePosition = transform.position;
        SetSprite(unitData.sprites[0]);
        unitModel = unit;
        unitModel.SetDisplay(this);
        EventDispatcher.RegisterEvent(BattleEvents.on_target_select.ToString() + unitModel.partyID, OnTargetSelect);
    }

    private void OnEnable()
    {

        BattleController.Instance.updateEntityAnimation += UpdateAnimation;
        BattleController.Instance.updateEntity += UpdateUnit;
    }

    private void OnTargetSelect(Dictionary<string, object> obj)
    {
        EventDispatcher.CallEvent(BattleEvents.on_target_select.ToString(), new Dictionary<string, object>() {
            { "target", this}
        });
    }

    private void OnDisable()
    {
        EventDispatcher.UnRegisterEvent(BattleEvents.on_target_select + unitModel.partyID, OnTargetSelect);
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
        if (BattleUI.Instance.IsInputting)
        {
            BattleController.Instance.SetPlayerCurrentTarget(unitModel);
        }
    }

    public void ResetDisplay()
    {
        unitAvatar.flipX = false;
        transform.position = homePosition;
        unitAvatar.DOFade(1f, 1f);
    }
}
