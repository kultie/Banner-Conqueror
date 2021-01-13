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
        UnitAnimationData data;
        public override void OnUpdate(float dt)
        {
            if (!customAnimation)
            {
                ResolvingTarget(e =>
                {
                    e.display.RequestAnimation(animState.ToString());
                });
            }
            else
            {
                ResolvingTarget(e =>
                {
                    int[] numbers = data.GetData();
                    Sprite[] anim_frames = new Sprite[numbers.Length];
                    for (int i = 0; i < anim_frames.Length; i++)
                    {
                        anim_frames[i] = e.data.sprites[numbers[i]];
                    }
                    var a = new AnimationData()
                    {
                        frames = anim_frames,
                        spf = data.spf,
                        loop = data.loop
                    };
                    e.display.RequestAnimation(a);
                });
            }
        }
    }
}