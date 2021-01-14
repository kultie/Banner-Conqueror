using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class AddEventToStoryBoard : CommonActionBase
    {
        public override void OnUpdate(float dt)
        {
            BattleController.Instance.AddEventToStoryBoard(new WaitEvent(5));
            BattleController.Instance.AddEventToStoryBoard(new LogEvent("hehe you will wait for more 2 sec"));
            BattleController.Instance.AddEventToStoryBoard(new WaitEvent(2));
            BattleController.Instance.AddEventToStoryBoard(new LogEvent("Finished"));
        }
    }
}