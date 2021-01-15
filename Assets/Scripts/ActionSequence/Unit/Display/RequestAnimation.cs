using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Unit.Display
{
    public class RequestAnimation : UnitDisplayActionBase
    {
        [SerializeField]
        bool customAnimation;
        [ShowIf("@!this.customAnimation")]
        [SerializeField]
        UnitAnimation animState;
        [ShowIf("@this.customAnimation")]
        [SerializeField]
        EntityAnimationData data;
        public override void OnUpdate(float dt)
        {
            if (!customAnimation)
            {
                owner.display.RequestAnimation(animState.ToString());
            }
            else
            {
                int[] numbers = data.GetData();
                Sprite[] anim_frames = new Sprite[numbers.Length];
                for (int i = 0; i < anim_frames.Length; i++)
                {
                    anim_frames[i] = owner.data.sprites[numbers[i]];
                }
                var a = new AnimationData()
                {
                    frames = anim_frames,
                    spf = data.spf,
                    loop = data.loop
                };
                owner.display.RequestAnimation(a);
            }
        }
    }
}