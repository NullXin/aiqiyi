
namespace AI.FSM
{
    /// <summary>
    /// 条件抽象类
    /// </summary>
    public abstract class FSMTrigger
    {
        /// <summary>
        /// 条件编号
        /// </summary>
        public FSMTriggerID triggerid;

        public FSMTrigger()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();

        ///<summary>检测条件达成</summary>
        public abstract bool HandleTrigger(BaseFSM fsm); 
    }
}
