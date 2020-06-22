using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : ManagerBase<BattleUI>
{
    public Button executeButton;
    public CharacterDisplay[] characterDisplays;
    public CharacterDisplay banner;
    public Transform commandStack;
    public CommandDisplay commandDisplay;
    protected override BattleUI GetInstance()
    {
        return this;
    }
    void Start()
    {
        BattleController.Instance.onPlayerTurn += PlayerTurnResolve;
        BattleController.Instance.onPlayerTurnExecute += PlayerTurnExecute;
    }

    public void InitCharacters(UnitEntity[] units, BannerUnit bannerUnit)
    {
        for (int i = 0; i < units.Length; i++)
        {
            characterDisplays[i].Init(units[i]);
        }
        banner.Init(bannerUnit);
    }

    private void PlayerTurnExecute()
    {
        executeButton.gameObject.SetActive(false);
    }

    private void PlayerTurnResolve()
    {
        executeButton.gameObject.SetActive(true);
    }

    public void AddCommandToStack(Sprite commandIcon, CommandQueue command)
    {
        CommandDisplay a = Instantiate(commandDisplay, commandStack);
        a.gameObject.SetActive(true);
        a.RegisterToCommand(command);
    }
}
