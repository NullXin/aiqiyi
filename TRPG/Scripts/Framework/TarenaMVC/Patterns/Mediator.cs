using System;
using System.Collections.Generic;
using TarenaMVC.Interfaces;
using UnityEngine;

namespace TarenaMVC.Patterns
{
    public class Mediator : IMediator, INotifier, IObserver
    {

        public static string NAME = "Mediator";
        public Mediator(object viewComponent = null)
        {
            this.mediatorName = NAME;
            this.viewComponent = viewComponent as Component;
        }
        /// <summary>
        /// 名字
        /// </summary>
        public string mediatorName{get;set; }
        /// <summary>
        /// 管理的组件
        /// </summary>
        public Component viewComponent { get; set; }
        /// <summary>
        /// 需要监听的消息列表
        /// </summary>
        /// <returns></returns>
        public virtual string[] ListNotificationInterests()
        {
            return new string[0];
        }
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="notification"></param>
        public virtual void HandleNotification(Notification notification)
        {
            
        }
        /// <summary>
        /// 注册时调用
        /// </summary>
        public virtual void OnRegister() { }
        /// <summary>
        /// 移除时调用
        /// </summary>
        public virtual void OnRemove()  {  }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public virtual void SendNotification(string name, object data=null)
        {
            
        }
    }
}
