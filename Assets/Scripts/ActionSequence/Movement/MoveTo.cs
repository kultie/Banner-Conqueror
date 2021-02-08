using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Movement
{
    public enum MoveType { Point, Targets, Offset }
    public class MoveTo : MovementAction
    {
        [SerializeField]
        MoveType moveType;

        [ShowIf("@this.moveType==MoveType.Point")]
        [SerializeField]
        Vector2 position;

        [ShowIf("@this.moveType==MoveType.Offset")]
        [SerializeField]
        Vector2 offset;

        [ShowIf("@this.moveType==MoveType.Targets")]
        [SerializeField]
        Vector2 targetOffset;

        [SerializeField]
        Ease easeType = Ease.Linear;
        [SerializeField]
        float duration = 1;

        public override Tween CreateTween()
        {
            Vector2 target = Vector2.zero;
            switch (moveType)
            {
                case MoveType.Point:
                    target = position;
                    break;
                case MoveType.Targets:
                    target = GetTargetAvgPosition() + targetOffset;
                    break;
                case MoveType.Offset:
                    target = (Vector2)owner.display.transform.position + offset;
                    break;
            }
            return owner.display.transform.DOMove(target, duration).SetEase(easeType);
        }      
    }
}