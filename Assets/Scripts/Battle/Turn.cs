﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    private List<Command> commands;
    private Command currentCommand;
    private Party party;
    private bool isFinished;
    public Turn(Party party)
    {
        isFinished = false;
        this.party = party;
    }

    public void Execute(List<Command> commands, BattleContext battleContext)
    {
        if (party.team == TeamSide.Player)
        {
            battleContext.IncreaseTurn();
        }
        this.commands = new List<Command>(commands);
        if (party.bannerUnit != null)
        {
            party.bannerUnit.TriggerBannerEffect(battleContext);
        }
    }

    public bool Finished()
    {
        return isFinished;
    }

    public void Update(float dt)
    {

        if (currentCommand == null && commands.Count == 0)
        {
            Debug.Log("All command has executed");
            isFinished = true;
            return;
        }

        if (currentCommand == null && commands.Count > 0)
        {
            currentCommand = commands[0];
            commands.RemoveAt(0);
        }
        if (currentCommand != null)
        {
            currentCommand.Update(dt);
            if (currentCommand.IsFinished())
            {
                currentCommand.OnFinished();
                currentCommand = null;
            }
        }
    }
}
