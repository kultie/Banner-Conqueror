using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.StoryBoard.Common
{
    public class DialogueEvent : CommonEvent
    {
        [SerializeField]
        Sprite actorAvatar;
        [SerializeField]
        string actorName;
        [SerializeField]
        string content;
        public override bool IsFinished()
        {
            return DialogueBox.Finished();
        }

        public override void Update(float dt)
        {
            DialogueBox.Update(dt);
            if (!DialogueBox.started && !DialogueBox.finished)
            {
                DialogueBox.Show(content, actorName, actorAvatar);
            }
        }
    }
}