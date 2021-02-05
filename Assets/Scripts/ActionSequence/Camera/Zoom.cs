using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.CameraSequence
{
    public class Zoom : CameraTween
    {
        [SerializeField]
        float value = 2;

        public override Tween CreateTween()
        {
            return CreateCameraZoom(value);
        }
    }
}