using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
namespace BC.ActionSequence.Display
{
    public class Fade : DisplayAction
    {
        [SerializeField]
        bool waitForFade;
        [SerializeField]
        Ease easeType = Ease.Linear;
        [SerializeField]
        bool activeOnTarget;
        [SerializeField]
        float fadeValue;
        [SerializeField]
        float duration = 1;
        protected bool finished;
        Tween tween;
        public override void OnEnter()
        {
            finished = false;
            if (activeOnTarget)
            {
                tween = DOTween.Sequence();
                for (int i = 0; i < targets.Length; i++)
                {
                    ((Sequence)tween).Join(targets[i].display.unitAvatar.DOFade(fadeValue, duration));
                }
            }
            else
            {
                tween = owner.display.unitAvatar.DOFade(fadeValue, duration);
            }
            tween.OnComplete(() =>
            {
                finished = true;
            });
            tween.Play();
        }

        public override bool IsBlock()
        {
            return waitForFade;
        }

        public override bool IsFinished()
        {
            return finished;
        }


    }
}