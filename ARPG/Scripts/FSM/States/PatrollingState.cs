using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace AI.FSM
{
    ///<summary>
    /// 巡逻
    ///</summary>
    public class PatrollingState : FSMState
    {
        private int index;

        protected override void Init()
        {
            StateID = FSMStateID.Patrolling;
        }
         
        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.isPatrolComplete = false;
            fsm.chAnim.SetBool(fsm.chStatus.animParams.Walk, true);
        }

        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.chAnim.SetBool(fsm.chStatus.animParams.Walk, false);
            fsm.StopMove();
        }

        public override void Action(FSMBase fsm)
        {
            base.Action(fsm);

            //如果到达目标点
            if (Vector3.Distance(fsm.transform.position, fsm.wapPoints[index].position) <= fsm.patrolArrivalDistance)
            {
                //最后一个路点
                if (index == fsm.wapPoints.Length - 1)
                {
                    switch (fsm.patrolMode)
                    {
                        case PatrolMode.Once:
                            fsm.isPatrolComplete = true;//完成巡逻
                            return;
                        case PatrolMode.PingPong:
                            Array.Reverse(fsm.wapPoints);
                            break;
                    }
                }
                //索引增加
                index = (index + 1) % fsm.wapPoints.Length;
            }
            //移动    
            fsm.MoveToTarget(fsm.wapPoints[index].position, fsm.patrolSpeed, fsm.patrolArrivalDistance);
            //旋转
            Vector3 lookPos= fsm.wapPoints[index].position;
            lookPos.y = fsm.transform.position.y;
            fsm.transform.LookPosition(lookPos, fsm.rotateSpeed);
        }

        /*
        public override void Action(FSMBase fsm)
        {
            base.Action(fsm);

            switch (fsm.patrolMode)
            {
                case PatrolMode.Once:
                    OncePatrolling(fsm);
                    break;
                case PatrolMode.Loop:
                    LoopPatrolling(fsm);
                    break;
                case PatrolMode.PingPong:
                    PingPongPatrolling(fsm);
                    break;
            }
            //旋转
            fsm.transform.LookPosition(fsm.wapPoints[index].position, fsm.rotateSpeed);
        }

        //单次巡逻
        private void OncePatrolling(FSMBase fsm)
        {
            //如果到达目标点
            if (Vector3.Distance(fsm.transform.position, fsm.wapPoints[index].position) <= fsm.patrolArrivalDistance)
            {
                //到达的目标点是 最后一个路点
                if (index == fsm.wapPoints.Length - 1)
                {
                    fsm.isPatrolComplete = true;//完成巡逻
                    return;
                } 
                index++;
            }
            //移动
            fsm.MoveToTarget(fsm.wapPoints[index].position, fsm.walkSpeed, fsm.patrolArrivalDistance);
        }

        //循环巡逻
        private void LoopPatrolling(FSMBase fsm)
        {
            //如果到达目标点
            if (Vector3.Distance(fsm.transform.position, fsm.wapPoints[index].position) <= fsm.patrolArrivalDistance)
            {//0   1   2 
                //index = (index + 1) <= fsm.wapPoints.Length - 1 ? index + 1 : 0;
                index = (index + 1) % fsm.wapPoints.Length;//3   %   3
            }
            //移动
            fsm.MoveToTarget(fsm.wapPoints[index].position, fsm.walkSpeed, fsm.patrolArrivalDistance);
        }

        //往返巡逻
        private void PingPongPatrolling(FSMBase fsm) 
        {
            //如果到达目标点
            if (Vector3.Distance(fsm.transform.position, fsm.wapPoints[index].position) <= fsm.patrolArrivalDistance)
            {
                //到达的目标点是 最后一个路点
                if (index == fsm.wapPoints.Length - 1)
                {
                    //A  B   C   -->  
                    //C  B   A
                    //A  B   C
                    Array.Reverse(fsm.wapPoints);//数组反转
                }
                //0  1  2  
                index = (index + 1) % fsm.wapPoints.Length;
            }
            //移动
            fsm.MoveToTarget(fsm.wapPoints[index].position, fsm.walkSpeed, fsm.patrolArrivalDistance);
        }
        */
    }
}