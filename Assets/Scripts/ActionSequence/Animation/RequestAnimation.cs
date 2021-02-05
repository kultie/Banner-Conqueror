using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Animation
{
    public class RequestAnimation : AnimationAction
    {
        [SerializeField]
        bool customAnimation;
        [ShowIf("@!this.customAnimation")]
        [SerializeField]
        UnitAnimation animState;
        [ShowIf("@this.customAnimation")]
        [SerializeField]
        EntityAnimationData data;
        protected override void OnUpdate(float dt)
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