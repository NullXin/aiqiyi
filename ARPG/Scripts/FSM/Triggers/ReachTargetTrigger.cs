using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 目标进入攻击范围
    /// </summary>
    public class ReachTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (fsm.targetTF == null) return false;
            return Vector3.Distance(fsm.transform.position, fsm.targetTF.position) <= fsm.chStatus.attackDistance;
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.ReachTarget;
        }
    }
}