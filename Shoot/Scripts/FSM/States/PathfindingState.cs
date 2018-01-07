using System;
using UnityEngine;
using Common;

namespace AI.FSM
{
    public class PathfindingState : FSMState
    {
        private int currentIndex;

        public override void Init()
        {
            stateid = FSMStateID.Pathfinding;
        }

        public override void Action(BaseFSM fsm)
        {
            //是否到达当前路点 
            if (Vector3.Distance(fsm.transform.position, fsm.wayPath[currentIndex]) < fsm.patrolArrivalDistance)
            {
                //如果是最后一个路点
                if (currentIndex == fsm.wayPath.Length - 1)
                { 
                    fsm.IsPatrolComplete = true;
                    return;
                }
                currentIndex++;
            } 
            //移动
            fsm.transform.position = Vector3.MoveTowards(fsm.transform.position, fsm.wayPath[currentIndex], fsm.runSpeed * Time.deltaTime);
            fsm.transform.LookPosition(fsm.wayPath[currentIndex],fsm.rotateSpeed);
        }

        public override void EnterState(BaseFSM fsm)
        { 
            fsm.IsPatrolComplete = false; 
            fsm.anim.SetBool(fsm.enemyStatus.animParams.run, true);
            currentIndex = 0;
        }

        public override void ExitState(BaseFSM fsm)
        { 
            fsm.anim.SetBool(fsm.enemyStatus.animParams.run, false);
        }
    }
}
