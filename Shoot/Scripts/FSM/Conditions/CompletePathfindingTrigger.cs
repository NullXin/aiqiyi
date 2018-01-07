

namespace AI.FSM
{
    public class CompletePathfindingTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggerID.CompletePathfinding;
        }

        public override bool HandleTrigger(BaseFSM fsm)
        {
            return fsm.IsPatrolComplete;
        }
    }
}
