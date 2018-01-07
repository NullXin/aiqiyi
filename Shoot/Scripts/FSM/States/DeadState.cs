using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 死亡状态
    /// </summary>
    public class DeadState : FSMState
    {
        public override void EnterState(BaseFSM fsm)
        {
            fsm.enabled = false; 
        } 

        public override void Init()
        {
            stateid = FSMStateID.Dead;
        }
    }
}
