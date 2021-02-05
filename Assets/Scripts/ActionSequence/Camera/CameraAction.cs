using BC.ActionSequence.Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BC.ActionSequence.CameraSequence
{
    public enum CameraTarget { Position, ActionTarget, ActionOwner }
    public abstract class CameraAction : AbilityActionBase
    {
        public static List<Tween> activeTweens = new List<Tween>();
        [SerializeField]
        protected bool waitForCamera;
        protected Tween tweener;
        protected bool finished;
        public abstract Tween CreateTween();
        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            base.Init(entity, targets, context);
        }

        public override void OnEnter()
        {
            finished = false;
            tweener = CreateTween();
            tweener.OnComplete(() =>
            {
                activeTweens.Remove(tweener);
                finished = true;
                tweener = null;
            });
        }

        public static void InsertTweener(Tween tweener)
        {
            activeTweens.Add(tweener);
        }
        protected override void OnUpdate(float dt)
        {
            if (tweener != null && !tweener.IsPlaying())
            {
                InsertTweener(tweener);
                tweener.Play();
            }
        }
        public override string DisplayOnEditor()
        {
            return "Camera";
        }

        public override bool IsBlock()
        {
            return waitForCamera;
        }

        public override bool IsFinished()
        {
            return finished;
        }

        public static bool AllTweenFinished()
        {
            return activeTweens.Count == 0;
        }
    }
}