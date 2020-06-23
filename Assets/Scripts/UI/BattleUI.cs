using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : ManagerBase<BattleUI>
{
    [SerializeField]
    private Button executeButton;
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

    }

    public void InitCharacters(UnitEntity[] units, BannerUnit bannerUnit)
    {
        for (int i = 0; i < units.Length; i++)
        {
            characterDisplays[i].Init(units[i]);
        }
        banner.Init(bannerUnit);
    }

    public void ShowExecuteButton(bool isShow)
    {
        executeButton.gameObject.SetActive(isShow);
    }

    public void AddCommandToStack(Sprite commandIcon, CommandQueue command)
    {
        CommandDisplay a = Instantiate(commandDisplay, commandStack);
        a.gameObject.SetActive(true);
        a.RegisterToCommand(command);
    }

    public void SetButtonFunction(Action function)
    {
        executeButton.onClick.RemoveAllListeners();
        executeButton.onClick.AddListener(() =>
        {
            function?.Invoke();
        });
    }
}
