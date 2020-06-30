using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXDisplay : MonoBehaviour
{
    public SpriteRenderer renderer;
    ParticleFXEntity entity;
    private void OnEnable()
    {
        BattleController.Instance.updateEntityAnimation += UpdateAnimation;
    }

    private void OnDisable()
    {
        BattleController.Instance.updateEntityAnimation -= UpdateAnimation;
    }

    public void SetEntity(ParticleFXEntity entity)
    {
        this.entity = entity;
    }

    private void UpdateAnimation(float dt)
    {
        entity.Update(dt);
        renderer.sprite = entity.GetSprite();
    }
}
