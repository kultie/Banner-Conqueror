using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.CameraSequence
{
    public class Offset : CameraTween
    {
        [SerializeField]
        Vector2 offset;
        public override Tween CreateTween()
        {
            return CreateCameraOffset(offset);
        }
    }
}