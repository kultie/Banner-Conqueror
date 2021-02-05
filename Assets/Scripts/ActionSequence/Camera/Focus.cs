using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.CameraSequence
{
    public class Focus : CameraTween
    {
        [SerializeField]
        CameraTarget targetType;

        [ShowIf("@this.targetType == CameraTarget.Position")]
        [SerializeField]
        Vector2 position;
        public override Tween CreateTween()
        {

            Vector3 cameraTarget = new Vector3(0, 0, -10);
            Vector2 targetPosition = Vector2.zero;
            switch (targetType)
            {
                case CameraTarget.Position:
                    targetPosition = position;
                    break;
                case CameraTarget.ActionTarget:
                    targetPosition = targets[0].display.transform.position;
                    break;
                case CameraTarget.ActionOwner:
                    targetPosition = owner.display.transform.position;
                    break;
            }
            cameraTarget.x = targetPosition.x;
            cameraTarget.y = targetPosition.y;
            return CreateCameraMove(cameraTarget);
        }
    }
}