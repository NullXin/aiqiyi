using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    ///<summary>
    /// 状态
    ///</summary>
    public abstract class FSMState
    {
        /// <summary>
        /// 状态编号
        /// </summary>
        public FSMStateID StateID { get; set; }
        //条件列表
        private List<FSMTrigger> triggerList;
        //映射表
        private Dictionary<FSMTriggerID, FSMStateID> map;

        public FSMState()
        {
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            triggerList = new List<FSMTrigger>();
            Init();
        }

        #region 管理状态
        //初始化
        protected abstract void Init(); 
        //添加映射(配置状态机时调用)
        public void AddMap(FSMTriggerID triggerID,FSMStateID stateID)
        {
            map.Add(triggerID, stateID);
            AddTriggerObject(triggerID);
        }
        //添加条件对象
        private void AddTriggerObject(FSMTriggerID triggerID)
        {
            Type type = Type.GetType("AI.FSM." + triggerID + "Trigger");
            var obj = Activator.CreateInstance(type) as FSMTrigger;
            triggerList.Add(obj);
        }
        /// <summary>
        /// 条件检测(状态机每帧调用)
        /// </summary>
        public void Reason(FSMBase fsm)
        {
            foreach (var item in triggerList)
            {
                if (item.HandleTrigger(fsm))
                {
                    FSMStateID stateId = map[item.TriggerID];
                    //通过状态机切换状态 
                    fsm.ChangeState(stateId);
                    return;
                }
            }
        } 
        #endregion

        #region 状态行为 
        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void EnterState(FSMBase fsm) { }
        /// <summary>
        /// 状态行为
        /// </summary>
        public virtual void Action(FSMBase fsm) { }
        /// <summary>
        /// 离开状态
        /// </summary>
        public virtual void ExitState(FSMBase fsm) { }
        #endregion 
    }
}