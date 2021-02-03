using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityContainer : MonoBehaviour
{
    [SerializeField]
    AbilityDisplay[] displays;
    public void Init(UnitEntity entity) {
        for (int i = 0; i < displays.Length; i++) {
            displays[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < entity.data.abilities.Length; i++) {
            displays[i].gameObject.SetActive(true);
            displays[i].Init(entity.data.abilities[i], entity);
        }
    }
}
