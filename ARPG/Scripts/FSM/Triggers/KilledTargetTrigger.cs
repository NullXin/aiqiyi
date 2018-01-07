using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;
namespace AI.FSM
{
    /// <summary>
    /// 击杀目标
    /// </summary>
    public class KilledTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            Transform tf = fsm.targetTF;
            if (!tf) return false;
            CharacterStatus cs = tf.GetComponent<CharacterStatus>();
            if (!cs) return false;
            return cs.HP <= 0;
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.KilledTarget;
        }
    }
}