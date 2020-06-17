using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : ManagerBase<BattleUI>
{
    public Button executeButton;
    protected override BattleUI GetInstance()
    {
        return this;
    }
    void Start()
    {
        BattleController.Instance.onPlayerTurn += PlayerTurnResolve;
        BattleController.Instance.onPlayerTurnExecute += PlayerTurnExecute;
    }

    private void PlayerTurnExecute()
    {
        executeButton.gameObject.SetActive(false);
    }

    private void PlayerTurnResolve()
    {
        executeButton.gameObject.SetActive(true);
    }
}
