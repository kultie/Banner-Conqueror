using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : ManagerBase<BattleController>
{
    public BattleContext context;

    public PartyDisplay playerParty;
    public PartyDisplay enemyParty;

    public UpdateEntity updateEntityAnimation;
    public UpdateEntity updateEntity;

    protected override BattleController GetInstance()
    {
        return this;
    }

    void Start()
    {
        UnitEntity[] unitEntities = new UnitEntity[] {
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            //new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle"))),
            new UnitEntity(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")))}
        ;
        UnitEntity bannerUnit = new BannerUnit(new UnitData(ResourcesManager.GetUnitData("f_arch_angle")));
        playerParty.Setup(new Party(unitEntities, bannerUnit, TeamSide.Player));
        enemyParty.Setup(new Party(unitEntities, bannerUnit, TeamSide.Enemy));
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        updateEntityAnimation?.Invoke(dt);
        updateEntity?.Invoke(dt);
    }
}
