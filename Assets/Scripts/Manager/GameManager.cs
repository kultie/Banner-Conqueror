using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    protected override GameManager GetInstance()
    {
        return this;
    }
}
