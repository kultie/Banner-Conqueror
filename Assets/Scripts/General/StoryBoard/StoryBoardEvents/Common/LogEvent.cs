using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.StoryBoard.Common
{
    public class LogEvent : CommonEvent
    {
        [SerializeField]
        string value;

        public override bool IsFinished()
        {
            return true;
        }

        public override void Update(float dt)
        {
            Debug.Log(value);
        }
    }
}