using Kultie.StateMachine;
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

    public StateMachine<BattleState, BattleContext> stateMachine { private set; get; }
    BattleContext battleContext;

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
        battleContext = GameManager.Instance.battleContext;
        stateMachine = new StateMachine<BattleState, BattleContext>(CreateBattleStates());

        if (battleContext == null)
        {
            UnitEntity[] playerUnits = new UnitEntity[] {
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_assasin")))};
            BannerUnit playerBannerUnit = new BannerUnit(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")));
            UnitEntity[] enemyUnits = new UnitEntity[] {
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_assasin"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("m_assasin")))};
            BannerUnit enemyBannerUnit = new BannerUnit(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")));
            battleContext = new BattleContext(this, new Party(playerUnits, playerBannerUnit, TeamSide.Player), new Party(enemyUnits, enemyBannerUnit, TeamSide.Enemy));
        }


        playerParty.Setup(battleContext.playerParty);
        enemyParty.Setup(battleContext.enemyParty);
        BattleUI.Instance.InitCharacters(battleContext.playerParty.mainUnit, battleContext.playerParty.bannerUnit);

        stateMachine.Change(BattleState.Start, battleContext);
    }

    Dictionary<BattleState, IState<BattleContext>> CreateBattleStates()
    {
        Dictionary<BattleState, IState<BattleContext>> states = new Dictionary<BattleState, IState<BattleContext>>();
        states[BattleState.Start] = new BattleStartState();
        states[BattleState.Input] = new BattleInputState();
        states[BattleState.EnemyTurn] = new BattleEnemyTurnState();
        states[BattleState.Result] = new BattleResultState();
        states[BattleState.TurnProcess] = new BattleTurnProcessState();
        return states;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        updateEntityAnimation?.Invoke(dt);
        updateEntity?.Invoke(dt);
        stateMachine.Update(dt);
        timer.Update(dt);
    }

    public void SetPlayerCurrentTarget(UnitEntity entity)
    {
        battleContext.SetPlayerCurrentTarget(entity);
    }

    public Command AddCommandQueue(UnitEntity caster, string actionID)
    {
        if (battleContext.playerCurrentTarget == null) {
            battleContext.SetPlayerCurrentTarget(battleContext.enemyParty.mainUnit[0]);
        }
        Command command = new Command(caster, new UnitEntity[] { battleContext.playerCurrentTarget }, actionID);
        battleContext.AddCommand(command);
        return command;
    }

    public Command AddCommandQueueAuto(UnitEntity caster, string actionID)
    {
        Command command = new Command(caster, (UnitEntity[])caster.variables["targets"], actionID);
        battleContext.AddCommand(command);
        return command;
    }

    public void RemoveCommand(Command command)
    {
        battleContext.RemoveCommand(command);
    }
}
