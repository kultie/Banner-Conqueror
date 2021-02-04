using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.StoryBoard
{
    public class StoryBoard
    {
        private List<StoryBoardEvent> events;

        public StoryBoard()
        {
            events = new List<StoryBoardEvent>();
        }

        public void AddToStoryBoard(StoryBoardEvent evt)
        {
            events.Add(evt);
            evt.OnAdd();
        }

        public void Update(float dt)
        {
            int deleteIndex = -1;
            for (int i = 0; i < events.Count; i++)
            {
                events[i].Update(dt);
                if (events[i].IsFinished())
                {
                    deleteIndex = i;
                    break;
                }
                if (events[i].IsBlock())
                {
                    break;
                }
            }
            if (deleteIndex != -1)
            {
                events.RemoveAt(deleteIndex);
            }
        }

        public bool IsFinished()
        {
            return events.Count == 0;
        }
    }
}