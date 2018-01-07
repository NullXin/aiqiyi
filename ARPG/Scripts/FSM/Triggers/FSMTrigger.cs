using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
	///<summary>
	/// 条件
	///</summary>
	public abstract class FSMTrigger
	{
        /// <summary>
        /// 条件编号
        /// </summary>
        public FSMTriggerID TriggerID { get; set; }

        public FSMTrigger()
        {
            Init();
        }

        /// <summary>
        /// 初始化(要求子类必须初始化条件编号)
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// 逻辑处理
        /// </summary>
        /// <returns></returns>
        public abstract  bool HandleTrigger(FSMBase fsm);
	}
}