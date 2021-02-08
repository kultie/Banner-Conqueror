using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Display
{
    public enum FaceDirection { Targets, Point, Direction }
    public class SetFacing : DisplayAction
    {
        [SerializeField]
        FaceDirection faceDirection;

        [ShowIf("@this.faceDirection == FaceDirection.Point")]
        [SerializeField]
        Vector2 point;

        [ShowIf("@this.faceDirection == FaceDirection.Direction")]
        [SerializeField]
        bool forward = true;

        public override void OnEnter()
        {
            Vector2 dir = Vector2.zero;
            switch (faceDirection)
            {
                case FaceDirection.Targets:
                    dir = GetTargetsDirection();
                    break;
                case FaceDirection.Point:
                    dir = point;
                    break;
                case FaceDirection.Direction:
                    dir = (Vector2)owner.display.transform.position + new Vector2(forward ? 1 : -1, 0);
                    break;
            }
            SetDirection(dir);
        }

        private Vector2 GetTargetsDirection()
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

        private void SetDirection(Vector2 target)
        {
            Vector2 currentPos = owner.display.transform.position;
            owner.display.unitAvatar.flipX = target.x > currentPos.x;
        }
    }
}