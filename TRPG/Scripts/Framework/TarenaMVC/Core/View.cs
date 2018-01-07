using System;
using System.Collections.Generic;
using TarenaMVC.Interfaces;
using TarenaMVC.Patterns;
using UnityEngine;

namespace TarenaMVC.Core
{
    public class View : IView
    {
        #region 单例模式
        protected static IView instance;

        public static IView GetInstance()
        {
            if (instance == null) instance = new View();
            return instance;
        }

        public View()
        {
            if (instance != null) throw new Exception( "请使用View.GetInstance()获取实例" );
            instance = this;
        }
        #endregion
        /// <summary>
        /// 存储所有的Mediator
        /// </summary>
        protected Dictionary<string, IMediator> mediatorMap = new Dictionary<string, IMediator>();
        
        /// <summary>
        /// 注册Mediator
        /// </summary>
        /// <param name="mediator"></param>
        public void RegisterMediator(IMediator mediator)
        {
            if ( HasMediator( mediator.mediatorName ) ) return; // 不重复注册同一个Mediator
            mediatorMap[mediator.mediatorName] = mediator; // 放到map里面
            mediator.OnRegister(); // 执行OnRegiser方法
            Debug.Log( "RegisterMediator:: " + mediator.mediatorName );
            string[] notifications = mediator.ListNotificationInterests(); // 找到所有的消息
            if( notifications.Length > 0) // 如果有
            {
                for (int i = 0 ; i < notifications.Length ; i++) // 则遍历一遍 添加到NotificationCenter里面
                {
                    Debug.Log( "AddObserver " + i + " " +  notifications[i] );
                    NotificationCenter.GetInstance().AddObserver( mediator, notifications[i] ); // 
                }
            }
        }
        /// <summary>
        /// 移除Mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        public IMediator RemoveMediator(string mediatorName)
        {
            IMediator mediator = HasMediator( mediatorName ) ? mediatorMap[mediatorName] : null;
            if( mediator != null) // 如果不为空
            {
                string[] notifications = mediator.ListNotificationInterests(); // 找到所有的消息
                if ( notifications.Length > 0) // 如果有
                {
                    for (int i = 0 ; i < notifications.Length ; i++) // 则遍历一遍 从NotificationCenter里面移除
                    {
                        NotificationCenter.GetInstance().RemoveObserver( mediator, notifications[i] );
                    }
                }
                mediator.OnRemove(); // 调用OnRemove方法
                mediatorMap[mediatorName] = null; // 置为空
            }
            return mediator;
        }
        /// <summary>
        /// 获取Mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        public IMediator RetrieveMediator(string mediatorName)
        {
            IMediator mediator = HasMediator( mediatorName ) ? mediatorMap[mediatorName] : null;
            return mediator;
        }

        /// <summary>
        /// 是否有mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        public bool HasMediator(string mediatorName)
        {
            return mediatorMap.ContainsKey( mediatorName );
        }
    }
}
