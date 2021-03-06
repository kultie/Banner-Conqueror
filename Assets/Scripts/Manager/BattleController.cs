﻿using Kultie.StateMachine;
using Kultie.TimerSystem;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BattleController : ManagerBase<BattleController>
{
    public PartyDisplay playerParty;
    public PartyDisplay enemyParty;

    public UpdateEntity updateEntityAnimation;
    public UpdateEntity updateEntity;

    public ParticleFXDisplay particleFXPrefab;
    public Transform particleContainer;

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
        particleFXPrefab.CreatePool();
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

            Party pParty = null;
            Party eParty = null;
            pParty = new Party(playerUnits, playerBannerUnit, TeamSide.Player, eParty);
            eParty = new Party(enemyUnits, enemyBannerUnit, TeamSide.Enemy, pParty);
            battleContext = new BattleContext(this, pParty, eParty);
        }


        playerParty.Setup(battleContext.playerParty);
        enemyParty.Setup(battleContext.enemyParty);
        BattleUI.Instance.InitCharacters(battleContext.playerParty.members, battleContext.playerParty.bannerUnit);

        stateMachine.Change(BattleState.Start, battleContext);
    }

    public UnitEntity[] GetTarget(TargetType targetType, UnitEntity caster)
    {
        Party enemyParty = battleContext.enemyParty;
        Party allyParty = battleContext.playerParty;
        if (!caster.isPlayerUnit)
        {
            enemyParty = battleContext.playerParty;
            allyParty = battleContext.enemyParty;
        }

        switch (targetType)
        {
            case TargetType.OneEnemy:
                return new UnitEntity[] { enemyParty.GetRandom() };
            case TargetType.AllEnemies:
                return enemyParty.members.Where(u => !u.IsDead()).ToArray();
            case TargetType.OneAlly:
                return new UnitEntity[] { allyParty.members.First(u => !u.IsDead()) };
            case TargetType.AllAllies:
                return allyParty.members.Where(u => !u.IsDead()).ToArray();
            case TargetType.Self:
                return new UnitEntity[] { caster };
            case TargetType.AllAlliesButSelf:
                return allyParty.members.Where(u => !u.IsDead() && u != caster).ToArray();
            case TargetType.All:
                return enemyParty.members.Union(allyParty.members).ToArray();
            default:
                return null;
        }
    }

    Dictionary<BattleState, IState<BattleContext>> CreateBattleStates()
    {
        Dictionary<BattleState, IState<BattleContext>> states = new Dictionary<BattleState, IState<BattleContext>>();
        states[BattleState.Start] = new BattleStartState();
        states[BattleState.Input] = new BattleInputState();
        states[BattleState.EnemyTurn] = new BattleEnemyTurnState();
        states[BattleState.Result] = new BattleResultState();
        states[BattleState.Event] = new BattleEventState();
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

    public void AddCommandQueue(UnitEntity caster, int index)
    {
        UnitEntity[] playerTarget = null;
        if (battleContext.playerCurrentTarget != null)
        {
            playerTarget = new UnitEntity[] { battleContext.playerCurrentTarget };
        }
        Command command = new Command(caster, playerTarget, caster.data.abilities[index], this);
        if (command.costData != null)
        {
            if (caster.CheckCost(command.costData))
            {
                caster.LoseCost(command.costData);
                battleContext.AddCommand(command);
                BattleUI.Instance.AddCommandToStack(null, command);
            }
            else
            {
                Debug.Log("Not enough cost");
            }
        }
        else
        {
            battleContext.AddCommand(command);
            BattleUI.Instance.AddCommandToStack(null, command);
        }
    }

    public void AddCommandQueueAuto(UnitEntity caster, int index)
    {
        Command command = new Command(caster, (UnitEntity[])caster.variables["targets"], caster.data.abilities[index], this);
        battleContext.AddCommand(command);
    }

    public void RemoveCommand(Command command)
    {
        battleContext.RemoveCommand(command);
    }

    public void CreateParticleFx(JSONNode data)
    {
        ParticleFXEntity entity = new ParticleFXEntity(data);
        var particle = particleFXPrefab.Spawn(particleContainer);
        particle.SetEntity(entity);
        particle.transform.localScale = Vector3.one;
        particle.gameObject.SetActive(true);
    }
}
