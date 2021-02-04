using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.StoryBoard.Common
{
    public abstract class CommonEvent : StoryBoardEvent
    {
        public override string DisplayOnEditor()
        {
            return "Common";
        }
    }
}