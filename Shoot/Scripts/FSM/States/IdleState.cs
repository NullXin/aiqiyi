using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    public class IdleState : FSMState
    {  
        public override void Init()
        {
            stateid = FSMStateID.Idle;
        }

        public override void EnterState(BaseFSM fsm)
        {
            fsm.anim.SetBool(fsm.enemyStatus.animParams.idle, true);
        }

        public override void ExitState(BaseFSM fsm)
        {
            fsm.anim.SetBool(fsm.enemyStatus.animParams.idle, false); 
        }
    }
}
