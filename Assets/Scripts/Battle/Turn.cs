using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    private List<CommandQueue> commands;
    private CommandQueue currentCommand;
    private Party party;
    private bool isFinished;
    public Turn()
    {
        isFinished = false;
    }

    public void Execute(List<CommandQueue> commands)
    {
        this.commands = new List<CommandQueue>(commands);

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
