using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDamageFXEntity : FXEntity
{
    public int value;
    public BattleDamageFXEntity(int value)
    {
        this.value = Mathf.Max(value, 0);
    }

    public override void Update(float dt)
    {

    }
}
