
namespace AI.FSM
{
    /// <summary>
    /// 生命为0
    /// </summary>
    public  class NoHealthTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerid = FSMTriggerID.NoHealth;
        }
        public override bool HandleTrigger(BaseFSM fsm)
        {
            return fsm.enemyStatus.HP <= 0;
        }
    }
}
