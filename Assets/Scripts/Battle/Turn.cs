using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public Queue<CommandQueue> commands;
    private CommandQueue currentCommand;
    Party party;
    bool turnStarted;
    public Turn(Party party)
    {
        this.party = party;
        OnTurnEnter();
        turnStarted = false;
    }

    private void OnTurnEnter()
    {
        Debug.Log("Entering turn for: " + party.team.ToString());
        Debug.Log("Running all action that resolve when turn enter here such as poison damage, burn damage, calculating debuff turn etc...");
        if (party.Lost())
        {
            EndTurn();
        }
    }

    private void OnTurnExit()
    {
        Debug.Log("current turn is ending");
    }

    public void ExecuteTurn(Queue<CommandQueue> commands)
    {
        turnStarted = true;
        Debug.Log("Prepare all command here");
        this.commands = commands;
        Debug.Log("Running all command and action here");

    }

    void EndTurn()
    {
        OnTurnExit();
        BattleController.Instance.OnTurnEnd();
    }

    public void Update(float dt)
    {
        if (BattleController.Instance.battleHasEnded)
        {
            return;
        }
        if (turnStarted)
        {
            if (currentCommand == null && commands.Count == 0)
            {
                Debug.Log("All command has executed");
                EndTurn();
                return;
            }
        }
        else
        {
            return;
        }

        if (currentCommand == null && commands.Count > 0)
        {
            currentCommand = commands.Dequeue();
        }
        if (currentCommand != null)
        {
            currentCommand.Update(dt);
            if (currentCommand.IsFinished())
            {
                currentCommand.owner.ResetAnimation();
                currentCommand = null;
            }
        }


    }
}
