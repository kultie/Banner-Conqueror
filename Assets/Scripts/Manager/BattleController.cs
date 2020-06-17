using Kultie.TimerSystem;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BattleController : ManagerBase<BattleController>
{
    public PartyDisplay playerParty;
    public PartyDisplay enemyParty;

    public UpdateEntity updateEntityAnimation;
    public UpdateEntity updateEntity;
    public Timer timer;

    TeamSide currenTeamThatTakeTurn;
    public Turn currentTurn;
    BattleContext battleContext;
    public bool battleHasEnded;
    public Action onPlayerTurn;
    public Action onPlayerTurnExecute;
    protected override BattleController GetInstance()
    {
        return this;
    }

    void Start()
    {
        timer = new Timer();
        StartBattle();
    }



    public void StartBattle()
    {
        battleHasEnded = false;
        UnitEntity[] playerUnits = new UnitEntity[] {
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")))}
            ;
        UnitEntity playerBannerUnit = new BannerUnit(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")));
        UnitEntity[] enemyUnits = new UnitEntity[] {
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")))}
    ;
        UnitEntity enemyBannerUnit = new BannerUnit(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")));
        battleContext = new BattleContext(new Party(playerUnits, playerBannerUnit, TeamSide.Player), new Party(enemyUnits, enemyBannerUnit, TeamSide.Enemy));
        playerParty.Setup(battleContext.playerParty);
        enemyParty.Setup(battleContext.enemyParty);
        CreatTurn(battleContext.playerParty);
    }

    public void CreatTurn(Party team)
    {
        if (team.team == TeamSide.Player)
        {
            onPlayerTurn?.Invoke();
        }
        currenTeamThatTakeTurn = team.team;
        currentTurn = new Turn(team);
    }
    public void ChangeTurn()
    {
        Party nextTeam = battleContext.playerParty;
        if (currenTeamThatTakeTurn == TeamSide.Player)
        {
            nextTeam = battleContext.enemyParty;
        }
        CreatTurn(nextTeam);
    }

    public void OnTurnEnd()
    {
        ResetAnimationForAllEntity();
        if (battleContext.playerParty.Lost())
        {
            BattleEnd(BattleResult.Lose);
        }
        else if (battleContext.enemyParty.Lost())
        {
            BattleEnd(BattleResult.Win);
        }
        else
        {
            ChangeTurn();
        }
    }

    private void ResetAnimationForAllEntity()
    {
        battleContext.playerParty.ResetAnimation();
        battleContext.enemyParty.ResetAnimation();
    }

    public void ExecuteTurn()
    {
        if (currenTeamThatTakeTurn == TeamSide.Player)
        {
            onPlayerTurnExecute?.Invoke();
        }
        Queue<CommandQueue> q = new Queue<CommandQueue>();
        q.Enqueue(new CommandQueue(battleContext.playerParty.mainUnit[0],
            new UnitEntity[] {
                battleContext.enemyParty.mainUnit[0]
            },
            new List<CommandAction>()
            {
                new CommandPlayAnimation(new Dictionary<string, object>(){
                    {"block", true },
                    {"anim_id", "attack_1" },
                }),
                new CommandWait(new Dictionary<string, object>(){
                    {"time", 0.24f },
                }),
                new CommandAttack(new Dictionary<string, object>()),
                new CommandWait(new Dictionary<string, object>(){
                    {"time", 1f }
                }),
                new CommandPlayAnimation(new Dictionary<string, object>(){
                    {"block", false },
                    {"anim_id", "idle" }
                }),
            }));
        q.Enqueue(new CommandQueue(battleContext.playerParty.mainUnit[0],
    new UnitEntity[] {
                battleContext.enemyParty.mainUnit[0]
    },
    new List<CommandAction>()
    {
                new CommandPlayAnimation(new Dictionary<string, object>(){
                    {"block", true },
                    {"anim_id", "attack_2" },
                }),
                new CommandWait(new Dictionary<string, object>(){
                    {"time", 0.24f },
                }),
                new CommandAttack(new Dictionary<string, object>()),
                new CommandWait(new Dictionary<string, object>(){
                    {"time", 1f }
                }),
                new CommandPlayAnimation(new Dictionary<string, object>(){
                    {"block", false },
                    {"anim_id", "idle" }
                }),
    }));
        q.Enqueue(new CommandQueue(battleContext.playerParty.mainUnit[0],
    new UnitEntity[] {
                battleContext.enemyParty.mainUnit[0]
    },
    new List<CommandAction>()
    {
                new CommandPlayAnimation(new Dictionary<string, object>(){
                    {"block", true },
                    {"anim_id", "attack_3" },
                }),
                new CommandWait(new Dictionary<string, object>(){
                    {"time", 0.24f },
                }),
                new CommandAttack(new Dictionary<string, object>()),
                new CommandWait(new Dictionary<string, object>(){
                    {"time", 1f }
                }),
                new CommandPlayAnimation(new Dictionary<string, object>(){
                    {"block", false },
                    {"anim_id", "idle" }
                }),
    }));
        currentTurn.ExecuteTurn(q);
    }

    public void BattleEnd(BattleResult result)
    {
        battleHasEnded = true;
        switch (result)
        {
            case BattleResult.Win:
                Debug.Log("Win");
                break;
            case BattleResult.Lose:
                Debug.Log("Lose");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        updateEntityAnimation?.Invoke(dt);
        updateEntity?.Invoke(dt);
        currentTurn.Update(dt);
        timer.Update(dt);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteTurn();
        }
    }
}
