using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class WithoutAttackRangeTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (!fsm.targetTF) return false;
            return Vector3.Distance(fsm.transform.position, fsm.targetTF.position) >= fsm.chStatus.attackDistance &&
                !fsm.chStatus.IsAttacking();
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.WithoutAttackRange;
        }
    }
}