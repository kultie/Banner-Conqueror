using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Display
{
    public class SetSprite : UnitDisplayActionBase
    {
        [SerializeField]
        int spriteIndex;
        public override void OnUpdate(float dt)
        {
            owner.display.SetSprite(spriteIndex);
        }
    }
}