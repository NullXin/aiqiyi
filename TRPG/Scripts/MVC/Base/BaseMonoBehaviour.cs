using System;
using System.Collections.Generic;
using UnityEngine;
using TarenaMVC.Patterns;

namespace TRPG.MVC
{   
    /// <summary>
    /// 封装发送消息的功能
    /// </summary>
    public class BaseMonoBehaviour : MonoBehaviour, INotifier
    {
        // 结果
        public const string SUCCESS = "Success";
        public const string FAILURE = "Failure";
        // 显示和隐藏
        public const string SHOW = "Show";
        public const string HIDE = "Hide";
        /// <summary>
        /// 获取ApplicationFacade
        /// </summary>
        protected ApplicationFacade facade
        {
            get { return ApplicationFacade.GetInstance(); }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SendNotification(string name, object data = null)
        {
            facade.SendNotification( name, data );
        }
    }
}
