using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CommandDisplay : MonoBehaviour, IPointerClickHandler
{
    Command command;
    [SerializeField]
    Image icon;
    public void OnPointerClick(PointerEventData eventData)
    {
        BattleController.Instance.RemoveCommand(command);
        command.ReturnCostToOwner();
        Destroy(gameObject);
    }

    public void RegisterToCommand(Command command)
    {
        this.command = command;
        icon.sprite = command.ability.icon;
        command.finishedCallback += DestroyCommanDisplay;
    }

    void DestroyCommanDisplay(UnitEntity owner, UnitEntity[] targets)
    {
        command.finishedCallback -= DestroyCommanDisplay;
        Destroy(gameObject);
    }


}
