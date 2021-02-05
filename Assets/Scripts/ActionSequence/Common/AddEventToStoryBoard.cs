using BC.StoryBoard;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class AddEventToStoryBoard : CommonActionBase
    {
        [SerializeField]
        [ValueDropdown("StoryBoardEventTree")]
        public StoryBoardEvent[] events;

        private IEnumerable StoryBoardEventTree()
        {
            return StoryBoardEvent.TreeView();
        }
        protected override void OnUpdate(float dt)
        {
            for (int i = 0; i < events.Length; i++)
            {
                BattleController.Instance.AddEventToStoryBoard(events[i]);          
            }
        }
    }
}