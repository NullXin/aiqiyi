using System;
using System.Collections.Generic;
using UnityEngine;
using TarenaMVC.Patterns;

namespace TarenaMVC.Interfaces
{
    public interface IMediator:IObserver
    {
        /// <summary>
        /// Mediator名称
        /// </summary>
        string mediatorName { get; set; }
        /// <summary>
        /// 管理的组件(UI)
        /// </summary>
        Component viewComponent { get; set; }
        /// <summary>
        /// 存放所有需要监听和处理的消息的一个数组
        /// </summary>
        /// <returns></returns>
        string[] ListNotificationInterests();
        /// <summary>
        /// 注册时调用
        /// </summary>
        void OnRegister();
        /// <summary>
        /// 移除时调用
        /// </summary>
        void OnRemove();
    }
}
