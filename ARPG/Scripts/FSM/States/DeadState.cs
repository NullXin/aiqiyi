using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    /// 死亡状态
    ///</summary>
    public class DeadState : FSMState
    {
        protected override void Init()
        {
            StateID = FSMStateID.Dead;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            Debug.Log("进入玩家死亡状态");
            fsm.chAnim.SetBool(fsm.chStatus.animParams.Death, false);
            fsm.enabled = false;//禁用状态机组件
        }
    }
}