using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kultie.StateMachine;
using System;

public class BattleContext : StateContextBase
{
    public BattleController battleController { private set; get; }
    public Party playerParty;
    public Party enemyParty;
    public int turnCount { private set; get; }
    public Turn currentTurn { private set; get; }
    public TeamSide currentTeam { private set; get; }
    public BattleResult battleResult { private set; get; }
    public List<CommandQueue> commandQueue { private set; get; }
    public UnitEntity playerCurrentTarget { private set; get; }
    public BattleContext(BattleController controller, Party playerParty, Party enemyParty)
    {
        battleController = controller;
        turnCount = 0;
        this.playerParty = playerParty;
        this.enemyParty = enemyParty;
    }

    public void ChangeParty()
    {
        Party nextParty = playerParty;
        if (currentTeam == TeamSide.Player)
        {
            nextParty = enemyParty;
        }
        SetTeam(nextParty);
        if (currentTeam == TeamSide.Player)
        {
            battleController.stateMachine.Change(BattleState.Input, this);
        }
        else
        {
            battleController.stateMachine.Change(BattleState.EnemyTurn, this);
        }
    }

    public void ExecuteTurn()
    {
        currentTurn.Execute(commandQueue);
        battleController.stateMachine.Change(BattleState.TurnProcess, this);
    }

    public void IncreaseTurn(int value = 1)
    {
        turnCount += value;
    }

    public void SetTeam(Party party)
    {
        currentTeam = party.team;
        currentTurn = new Turn();
        commandQueue = new List<CommandQueue>();
    }

    public void AddCommand(CommandQueue command)
    {
        commandQueue.Add(command);
    }

    public void RemoveCommand(CommandQueue command)
    {
        commandQueue.Remove(command);
    }

    public bool BattleOver()
    {
        if (playerParty.Lost())
        {
            battleResult = BattleResult.Lose;
            return true;
        }
        if (enemyParty.Lost())
        {
            battleResult = BattleResult.Win;
            return true;
        }
        //Retreat
        //Draw
        return false;
    }

    public void SetPlayerCurrentTarget(UnitEntity target)
    {
        playerCurrentTarget = target;
    }
}
