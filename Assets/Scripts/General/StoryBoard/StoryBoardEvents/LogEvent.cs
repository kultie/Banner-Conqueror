using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEvent : StoryBoardEvent
{
    string value;
    public LogEvent(string value)
    {
        this.value = value;
    }

    public override bool IsFinished()
    {
        return true;
    }

    public override void Update(float dt)
    {
        Debug.Log(value);
    }
}
