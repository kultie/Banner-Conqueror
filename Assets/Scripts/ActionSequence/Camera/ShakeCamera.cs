using DG.Tweening;
using UnityEngine;

namespace BC.ActionSequence.CameraSequence
{
    public class ShakeCamera : CameraTween
    {
        [SerializeField]
        float strength = 10;
        [SerializeField]
        int vibrator = 10;
        [SerializeField]
        int randomness = 10;
        public override void Init(UnitEntity entity, UnitEntity[] targets, UnitAbility context)
        {
            base.Init(entity, targets, context);
        }

        public override Tween CreateTween()
        {
            return Camera.main.DOShakePosition(time, new Vector3(strength, strength, 0), vibrator, randomness);
        }
    }
}