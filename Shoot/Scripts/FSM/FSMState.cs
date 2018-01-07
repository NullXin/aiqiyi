using System;
using System.Collections.Generic;

namespace AI.FSM
{
    /// <summary>
    /// 抽象的状态类
    /// </summary>
    public abstract class FSMState
    {
        ///<summary>状态编号</summary>
        public FSMStateID stateid;
        ///<summary>条件列表</summary>
        private List<FSMTrigger> triggers;
        ///<summary>转换映射表</summary>
        private Dictionary<FSMTriggerID, FSMStateID> map;
        ///<summary>初始化</summary>
        public abstract void Init();

        public FSMState()
        {
            triggers = new List<FSMTrigger>();
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            Init();
        }

        ///<summary>添加条件映射</summary>
        public void AddMap(FSMTriggerID fsmTriggerId, FSMStateID fsmStateId)
        {
            map.Add(fsmTriggerId, fsmStateId);
            AddTriggerObject(fsmTriggerId);

            //if (!map.ContainsKey(fsmTriggerId))
            //{
            //    map.Add(fsmTriggerId, fsmStateId);
            //    AddTriggerObject(fsmTriggerId);
            //}
            //else
            //{ 
            //    map[fsmTriggerId] = fsmStateId;
            //}
        }
         
        ///<summary>添加条件对象</summary>
        private void AddTriggerObject(FSMTriggerID fsmTriggerId)
        {
            var type = Type.GetType("AI.FSM." + fsmTriggerId + "Trigger");
            if (type != null)
            {
                var triggerObj = Activator.CreateInstance(type) as FSMTrigger;
                triggers.Add(triggerObj);
            }
        }

        ///<summary>删除条件映射</summary>
        public void RemoveTrigger(FSMTriggerID fsmTriggerId)
        {
            if (map.ContainsKey(fsmTriggerId))
            {
                map.Remove(fsmTriggerId);
                RemoveTriggerObject(fsmTriggerId);
            }
        }
      
        ///<summary>删除条件对象</summary>
        private void RemoveTriggerObject(FSMTriggerID fsmTriggerId)
        {
            triggers.RemoveAll(p => p.triggerid == fsmTriggerId);
        }
      
        ///<summary>根据条件获取对应的输出状态</summary>
        private FSMStateID GetOutputState(FSMTriggerID fsmTriggerId)
        {
            if (map.ContainsKey(fsmTriggerId))
                return map[fsmTriggerId];
            return FSMStateID.None;
        }

        ///<summary>条件检测</summary>
        public virtual void Reason(BaseFSM fsm)
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                //如果当前条件满足，则切换到对应的状态
                if (triggers[i].HandleTrigger(fsm))
                {
                    //根据条件获取状态ID
                    var stateID = GetOutputState(triggers[i].triggerid);
                    //通过状态机切换状态
                    fsm.ChangActiveState(stateID);
                    return;
                }
            }
        }

        ///<summary>进入状态</summary>
        public virtual void EnterState(BaseFSM fsm) { }

        ///<summary>退出状态</summary>
        public virtual void ExitState(BaseFSM fsm) { }

        ///<summary>状态行为</summary>
        public virtual void Action(BaseFSM fsm) { } 
    }
}
