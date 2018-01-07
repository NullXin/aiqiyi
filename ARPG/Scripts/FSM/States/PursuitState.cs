using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace AI.FSM
{
    /// <summary>
    /// 追逐状态
    /// </summary>
    public class PursuitState : FSMState
    {
        protected override void Init()
        {
            StateID = FSMStateID.Pursuit;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            Debug.Log("进入追逐状态");
            if (fsm.targetTF == null) return;
            fsm.chAnim.SetBool(fsm.chStatus.animParams.Run, true);
        }

        public override void Action(FSMBase fsm)
        {
            base.Action(fsm);
            if (fsm.targetTF == null) return;
            fsm.MoveToTarget(fsm.targetTF.position, fsm.moveSpeed, fsm.chStatus.attackDistance);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.chAnim.SetBool(fsm.chStatus.animParams.Run, false);
            fsm.StopMove();
        }



    }
}