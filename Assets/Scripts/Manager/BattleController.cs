using Kultie.EventDispatcher;
using Kultie.StateMachine;
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
    public BattleDamageFXDisplay damageFXPrefab;
    public Transform fxContainer;

    public Timer timer;
    public StoryBoard storyBoard { private set; get; }

    public StateMachine<BattleState, BattleContext> battleStateMachine { private set; get; }
    public StateMachine<GameState, GameContext> gameStateMachine { private set; get; }
    BattleContext battleContext;
    GameContext gameContext;

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
        storyBoard = new StoryBoard();
        battleContext = GameManager.Instance.battleContext;
        gameStateMachine = new StateMachine<GameState, GameContext>(CreateGameStates());
        battleStateMachine = new StateMachine<BattleState, BattleContext>(CreateBattleStates());
        gameContext = new GameContext(battleStateMachine, storyBoard);
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
        gameStateMachine.Change(GameState.Battle, gameContext);
        battleStateMachine.Change(BattleState.Start, battleContext);
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
        states[BattleState.TurnProcess] = new BattleTurnProcessState();
        return states;
    }
    private Dictionary<GameState, IState<GameContext>> CreateGameStates()
    {
        Dictionary<GameState, IState<GameContext>> states = new Dictionary<GameState, IState<GameContext>>();
        states[GameState.Event] = new GameEventState();
        states[GameState.Battle] = new GameBattleState();
        return states;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        updateEntityAnimation?.Invoke(dt);
        updateEntity?.Invoke(dt);
        gameStateMachine.Update(dt);
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

    public void AddCommandQueue(UnitEntity caster, UnitAbility ability)
    {
        UnitEntity[] playerTarget = null;
        if (battleContext.playerCurrentTarget != null)
        {
            playerTarget = new UnitEntity[] { battleContext.playerCurrentTarget };
        }
        Command command = new Command(caster, playerTarget, ability, this);
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
        EventDispatcher.CallEvent(BattleEvents.on_ability_cancel.ToString() + command.owner.partyID + command.ability.GetInstanceID(), null);
        battleContext.RemoveCommand(command);
    }

    public void AddPlayerPartyAttackCommand()
    {
        UnitEntity[] playerTarget = null;
        if (battleContext.playerCurrentTarget != null)
        {
            playerTarget = new UnitEntity[] { battleContext.playerCurrentTarget };
        }
        for (int i = 0; i < battleContext.playerParty.members.Length; i++)
        {
            var p = battleContext.playerParty.members[i];
            Command cmd = new Command(p, playerTarget, p.data.attack, this);
            battleContext.AddCommand(cmd);
        }
    }

    public void CreateParticleFx(Sprite[] sprites, EntityAnimationData data)
    {
        ParticleFXEntity entity = new ParticleFXEntity(sprites, data);
        var particle = particleFXPrefab.Spawn(fxContainer);
        particle.SetEntity(entity);
        particle.transform.localScale = Vector3.one;
        particle.gameObject.SetActive(true);
    }

    public void CreateBattleDamageFX(int value, UnitEntity target)
    {
        BattleDamageFXEntity e = new BattleDamageFXEntity(value);
        var dmg = damageFXPrefab.Spawn(fxContainer);
        dmg.SetEntity(e);
        dmg.transform.localScale = Vector3.one;
        dmg.transform.position = target.display.transform.position;
        dmg.gameObject.SetActive(true);
    }

    public void AddEventToStoryBoard(StoryBoardEvent evt)
    {
        storyBoard.AddToStoryBoard(evt);
    }

    public void AddEventsToStoryBoard(StoryBoardEvent[] evt)
    {
        for (int i = 0; i < evt.Length; i++)
        {
            storyBoard.AddToStoryBoard(evt[i]);
        }
    }
}
