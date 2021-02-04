using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Linq;
namespace BC.StoryBoard
{
    [Serializable]
    public abstract class StoryBoardEvent
    {
        public virtual void OnAdd() { }
        public virtual string DisplayOnEditor()
        {
            return "Event";
        }
        public abstract void Update(float dt);

        public abstract bool IsFinished();

        public virtual bool IsBlock()
        {
            return true;
        }

        public static IEnumerable TreeView()
        {
            ValueDropdownList<StoryBoardEvent> result = new ValueDropdownList<StoryBoardEvent>();
            var q = typeof(StoryBoardEvent).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsGenericTypeDefinition)
                .Where(x => typeof(StoryBoardEvent).IsAssignableFrom(x));

            foreach (var e in q)
            {
                StoryBoardEvent instance = (StoryBoardEvent)Activator.CreateInstance(e);
                result.Add(instance.DisplayOnEditor() + "/" + e.Name, (StoryBoardEvent)Activator.CreateInstance(e));
            }
            return result;
        }
    }
}