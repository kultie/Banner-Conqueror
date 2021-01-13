using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : ManagerBase<DataManager>
{
    [SerializeField]
    Databox.DataboxObject gameData;
    private void Awake()
    {
        gameData.LoadDatabase();
    }
    protected override DataManager GetInstance()
    {
        return this;
    }

}
