using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Display
{
    public class SetSprite : UnitDisplayActionBase
    {
        [SerializeField]
        int spriteIndex;
        protected override void OnUpdate(float dt)
        {
            owner.display.SetSprite(spriteIndex);
        }
    }
}