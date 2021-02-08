using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BC.ActionSequence.Common
{
    public class SetTimeScale : CommonActionBase
    {
        [SerializeField]
        float value = 1;
        public override void OnEnter()
        {
            Time.timeScale = value;
        }
    }
}