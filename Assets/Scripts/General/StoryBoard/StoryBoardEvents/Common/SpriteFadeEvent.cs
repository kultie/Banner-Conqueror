using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.StoryBoard.Common
{
    public class SpriteFadeEvent : CommonEvent
    {
        public UnitEntity unit;
        public float fadeTime;
        private float time;

        public static SpriteFadeEvent CreateInstance(UnitEntity unit, float fadeTime)
        {
            var a = new SpriteFadeEvent();
            a.unit = unit;
            a.fadeTime = fadeTime;
            a.time = fadeTime;
            return a;
        }

        public override bool IsFinished()
        {
            return fadeTime <= 0;
        }

        public override void Update(float dt)
        {
            fadeTime -= dt;
            unit.display.SetSpriteAlpha(fadeTime / time);
        }

        public override bool IsBlock()
        {
            return false;
        }

        public override void OnAdd()
        {
            time = fadeTime;
        }
    }
}