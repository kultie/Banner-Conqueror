using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Movement
{
    public abstract class MovementAction : AbilityActionBase
    {
        public static List<Tween> activeTweens = new List<Tween>();

        [SerializeField]
        protected bool waitForMovement;
        protected Tween tweener;
        protected bool finished;
        public abstract Tween CreateTween();

        public override string DisplayOnEditor()
        {
            return "Movement";
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

        public override bool IsBlock()
        {
            return waitForMovement;
        }

        public override bool IsFinished()
        {
            return finished;
        }

        public static bool AllTweenFinished()
        {
            return activeTweens.Count == 0;
        }

        protected Vector2 GetTargetAvgPosition()
        {
            Vector2 pos = Vector2.zero;
            for (int i = 0; i < targets.Length; i++)
            {
                pos += (Vector2)targets[i].display.transform.position;
            }
            if (owner.party.team == TeamSide.Enemy)
            {
                pos.x = -pos.x;
            }
            return pos / targets.Length;
        }
    }
}