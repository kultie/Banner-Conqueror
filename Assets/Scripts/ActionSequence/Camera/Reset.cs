using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.CameraSequence
{
    public class Reset : CameraTween
    {
        public override Tween CreateTween()
        {
            Vector3 pos = new Vector3(0, 0, -10);
            float orthorSize = 4;
            Sequence seq = DOTween.Sequence();
            if (Camera.main.transform.position != pos)
            {
                seq.Join(CreateCameraMove(pos));
            }
            if (Camera.main.orthographicSize != orthorSize)
            {
                seq.Join(CreateCameraZoom(orthorSize));
            }
            return seq;
        }
    }
}