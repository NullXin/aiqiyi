using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    /// 待机状态
    ///</summary>
    public class IdleState : FSMState
    {
        protected override void Init()
        {
            StateID = FSMStateID.Idle;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            Debug.Log("进入初始化状态");
            fsm.chAnim.SetBool(fsm.chStatus.animParams.Idle, true);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.chAnim.SetBool(fsm.chStatus.animParams.Idle, false);
        }
    }
}