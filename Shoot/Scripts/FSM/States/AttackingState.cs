using UnityEngine;
using wzx;

namespace AI.FSM
{
    /// <summary>
    /// 攻击状态类
    /// </summary>
    public class AttackingState : FSMState
    {
        private float attackTime;
        private wzx.Gun gun;
        public override void Init()
        {
            stateid = FSMStateID.Attacking;
        }

        public override void EnterState(BaseFSM fsm)
        {
            base.EnterState(fsm);
            gun = fsm.GetComponent<wzx.Gun>();
            fsm.GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DoFiring;
        }

        public override void Action(BaseFSM fsm)
        {
            //按照指定时间间隔，使用技能
            attackTime += Time.deltaTime;
            if (attackTime > fsm.attackInterval)
            {
                fsm.anim.SetBool(fsm.enemyStatus.animParams.attack, true);
                attackTime = 0;
            }
            LookTarget(fsm);
        }

        public override void ExitState(BaseFSM fsm)
        {
            base.ExitState(fsm);
            fsm.GetComponentInChildren<AnimationEventBehaviour>().attackHandler -= DoFiring;
        }

        protected void DoFiring()
        {
            gun.Firing(Camera.main.transform.position - gun.firePointTF.position);
        } 

        private void LookTarget(BaseFSM fsm)
        {
            Vector3 targetPos = fsm.playerTF.position;
            targetPos.y = fsm.transform.position.y;
            fsm.transform.LookAt(targetPos);
        }
    }
}
