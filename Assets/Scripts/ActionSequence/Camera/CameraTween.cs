using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.CameraSequence
{
    public abstract class CameraTween : CameraAction
    {
        [SerializeField]
        protected float time = 1f;
        [SerializeField]
        Ease easeType = Ease.Linear;

        protected Tweener CreateCameraMove(Vector3 position)
        {
            return Camera.main.transform.DOMove(position, time).SetEase(easeType);
        }

        protected Tweener CreateCameraOffset(Vector2 offset)
        {
            Vector3 camPos = Camera.main.transform.position;
            Vector3 targetPos = camPos + (Vector3)offset;
            return CreateCameraMove(targetPos);
        }

        protected Tweener CreateCameraZoom(float value)
        {
            return Camera.main.DOOrthoSize(value, time).SetEase(easeType);
        }
    }
}