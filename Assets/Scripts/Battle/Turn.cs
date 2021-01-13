using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    private List<Command> commands;
    public Command currentCommand { private set; get; }
    private Party party;
    private BattleContext context;
    public Turn(Party party)
    {
        this.party = party;
    }

    public void Execute(List<Command> commands, BattleContext battleContext)
    {
        context = battleContext;
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
        return commands.Count == 0 && CurrentCommandFinished();
    }

    public void ProcessNextCommand()
    {
        currentCommand = commands[0];
        currentCommand.ResolveTarget();
        currentCommand.Execute();
        commands.RemoveAt(0);
    }

    public void ProcessCurrentCommand(float dt)
    {
        currentCommand.Update(dt);
        if (currentCommand.IsFinished())
        {
            currentCommand.OnFinished();
            currentCommand = null;
        }
    }

    public bool CurrentCommandFinished()
    {
        if (currentCommand == null)
        {
            return true;
        }
        return currentCommand.IsFinished();
    }
}
