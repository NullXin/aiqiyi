 
namespace AI.FSM
{
    public class KilledTargetTrigger : FSMTrigger
    {
        /// <summary>
        /// 目标被打死
        /// </summary>
        public override void Init()
        {
            triggerid = FSMTriggerID.KilledTarget;
        }

        public override bool HandleTrigger(BaseFSM fsm)
        {
            return fsm.playerTF.GetComponent<CharacterStatus>().HP <= 0;
        }
    }
}
