﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public abstract class StoryBoardEvent
{
    public abstract void Update(float dt);

    public abstract bool IsFinished();

    public virtual bool IsBlock()
    {
        return true;
    }
}
