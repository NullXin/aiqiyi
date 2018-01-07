using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace AI.FSM
{
    /// <summary>
    /// 攻击状态
    /// </summary>
    public class AttackingState : FSMState
    {
        private float attackTime;
        protected override void Init()
        {
            StateID = FSMStateID.Attacking;
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            Debug.Log("进入攻击状态");
        }
        public override void Action(FSMBase fsm)
        {
            base.Action(fsm);
            if (!fsm.targetTF) return;
            //如果到了攻击时间
            if (attackTime <= Time.time)
            {
                //随机攻击
                fsm.skillSystem.UseRandomSkill();
                //设置下次攻击时间
                attackTime = Time.time + fsm.chStatus.attackInterval;
                //朝向目标
            }
            fsm.transform.LookPosition(fsm.targetTF.position, 30);
        }
    }
}