using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;
namespace BC.ActionSequence.Effect
{
    public enum TargetType { Self, Targets, Point }
    public class CreateEffect : EffectAction
    {
        [SerializeField]
        TargetType targetType;
        [SerializeField]
        Sprite[] sprites;
        [SerializeField]
        EntityAnimationData animData;
        [ShowIf("@this.targetType == TargetType.Point")]
        [SerializeField]
        Vector2 position;
        [ShowIf("@this.targetType == TargetType.Self || this.targetType == TargetType.Targets")]
        [SerializeField]
        Vector2 offset;
        [SerializeField]
        float scale = 1;
        [SerializeField]
        bool flip;
        ParticleFXEntity[] entities;
        public override void OnEnter()
        {
            Vector2 position = Vector2.zero;
            switch (targetType)
            {
                case TargetType.Self:
                    position = (Vector2)owner.display.transform.position + offset;
                    entities = new ParticleFXEntity[1];
                    entities[0] = BattleController.Instance.CreateParticleFx(sprites, animData, position, scale, flip);
                    break;
                case TargetType.Targets:
                    entities = new ParticleFXEntity[targets.Length];
                    for (int i = 0; i < entities.Length; i++)
                    {
                        position = (Vector2)targets[i].display.transform.position + offset;
                        entities[i] = BattleController.Instance.CreateParticleFx(sprites, animData, position, scale, flip);
                    }
                    break;
                case TargetType.Point:
                    position = this.position;
                    entities = new ParticleFXEntity[1];
                    entities[0] = BattleController.Instance.CreateParticleFx(sprites, animData, position, scale, flip);
                    break;
            }
        }

        public override bool IsFinished()
        {
            return entities.All(e => e.FinishedAnim());
        }


    }
}