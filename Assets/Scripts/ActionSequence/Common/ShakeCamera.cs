using DG.Tweening;
using UnityEngine;

namespace BC.ActionSequence.Common
{
    public class ShakeCamera : CommonActionBase
    {
        [SerializeField]
        bool isBlock;
        [SerializeField]
        float duration;
        [SerializeField]
        float strength;
        [SerializeField]
        int vibrator;
        [SerializeField]
        int randomness;

        bool started;
        bool isFinished;
        public override void Init(UnitEntity entity, UnitEntity[] targets)
        {
            base.Init(entity, targets);
            started = false;
            isFinished = false;
        }
        public override void OnUpdate(float dt)
        {
            if (!started)
            {
                started = true;
                Camera.main.DOShakePosition(duration, new Vector3(strength, strength, 0), vibrator, randomness).OnComplete(() =>
                {
                    isFinished = true;
                });
            }
        }

        public override bool IsFinished()
        {
            return isFinished;
        }

        public override bool IsBlock()
        {
            return isBlock;
        }
    }
}