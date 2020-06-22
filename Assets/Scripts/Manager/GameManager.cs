using Kultie.TimerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Timer timer;
    public Dictionary<string, Entity> allEntities = new Dictionary<string, Entity>();
    public BattleContext battleContext;

    private void Start()
    {
        timer = new Timer();

        UnitEntity[] playerUnits = new UnitEntity[] {
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_assasin")))};
        BannerUnit playerBannerUnit = new BannerUnit(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")));
        UnitEntity[] enemyUnits = new UnitEntity[] {
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_assasin"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("m_assasin")))};
        BannerUnit enemyBannerUnit = new BannerUnit(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")));
        battleContext = new BattleContext(new Party(playerUnits, playerBannerUnit, TeamSide.Player), new Party(enemyUnits, enemyBannerUnit, TeamSide.Enemy));

    }

    private void Update()
    {
        float dt = Time.deltaTime;
        timer.Update(dt);
    }
}
