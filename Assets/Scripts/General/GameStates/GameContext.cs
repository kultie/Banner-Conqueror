using BC.StoryBoard;
using Kultie.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : StateContextBase
{
    public StateMachine<BattleState, BattleContext> battleStateMachine { private set; get; }
    public StoryBoard storyBoard { private set; get; }
    public GameContext(StateMachine<BattleState, BattleContext> battleStates, StoryBoard storyBoard)
    {
        battleStateMachine = battleStates;
        this.storyBoard = storyBoard;
    }

    public void ChangeGameState(GameState state)
    {
        BattleController.Instance.gameStateMachine.Change(state, this);
    }
}
