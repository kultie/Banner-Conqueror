using Kultie.TimerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    public Timer timer;
    public Dictionary<string, Entity> allEntities = new Dictionary<string, Entity>();
    protected override GameManager GetInstance()
    {
        return this;
    }

    private void Start()
    {
        timer = new Timer();
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        timer.Update(dt);
    }
}
