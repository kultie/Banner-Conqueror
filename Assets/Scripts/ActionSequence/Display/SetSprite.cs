using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Display
{
    public class SetSprite : DisplayAction
    {
        [SerializeField]
        int spriteIndex;
        protected override void OnUpdate(float dt)
        {
            owner.display.SetSprite(spriteIndex);
        }
    }
}