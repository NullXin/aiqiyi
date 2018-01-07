using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    /// 没有生命值
    ///</summary>
    public class NoHealthTrigger : FSMTrigger
    {
        protected override void Init()
        {
            TriggerID = FSMTriggerID.NoHealth;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.chStatus.HP <= 0;
        }
    }
}