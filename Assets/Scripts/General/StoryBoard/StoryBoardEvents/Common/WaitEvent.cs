using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.StoryBoard.Common
{
    public class WaitEvent : CommonEvent
    {
        [SerializeField]
        float time;
        float currentTime;
        public override bool IsFinished()
        {
            return currentTime <= 0;
        }

        public override void Update(float dt)
        {
            currentTime -= dt;
        }

        public override void OnAdd()
        {
            currentTime = time;
        }
    }
}