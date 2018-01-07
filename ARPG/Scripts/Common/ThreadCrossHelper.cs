using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    /// <summary>
    /// 线程交叉访问助手（挂在主游戏物体上能用）
    /// </summary>
    public class ThreadCrossHelper : MonoSingleton<ThreadCrossHelper>
    {
        public struct DelayedItem
        {
            public Action Action { get; set; }
            public DateTime Time { get; set; }
        }
        //1.定义延迟行为列表
        public List<DelayedItem> delayedItemActionList;

        public override void Init()
        {
            base.Init();
            delayedItemActionList = new List<DelayedItem>();
        }

        //在Update中轮询判定遍历
        private void Update()
        {
            lock(delayedItemActionList)
            {
                for (int i = delayedItemActionList.Count - 1; i >= 0; i--)
                {
                    if (delayedItemActionList[i].Time >= DateTime.Now) continue;
                    delayedItemActionList[i].Action();//执行委托
                    delayedItemActionList.RemoveAt(i);//移除委托
                }
            }
        }

        /// <summary>
        /// 为辅助线程提供，在主线程中执行逻辑
        /// </summary>
        /// <param name="action">委托</param>
        /// <param name="delay">延迟时间</param>
        public void ExecuteOnMainThread(Action action, float delay = 0)
        {
            var item = new DelayedItem() { Action = action, Time = DateTime.Now.AddSeconds(delay) };
            delayedItemActionList.Add(item);
        }
    }
}