using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TarenaMVC.Interfaces;
using TarenaMVC.Core;
using TarenaMVC.Patterns;

namespace TarenaMVC.Core
{
    /// <summary>
    /// TarenaMVC对外的唯一接口
    /// </summary>
    public class Facade : IFacade,INotifier
    {
        #region 单例模式
        protected IModel model;
        protected IView view;
        protected static IFacade instance;
        public static IFacade GetInstance()
        {
            if( instance == null)
            {
                instance = new Facade();
            }
            return instance;
        }
        public Facade()
        {
            if (instance != null) throw new Exception( "Facade只能通过GetInstance()获取" );
            instance = this;
            model = Model.GetInstance(); // 实例化Model
            view = View.GetInstance();// 实例化View
            InitModel();
        }
        /// <summary>
        /// 初始化Model(注册Proxy)
        /// </summary>
        protected virtual void InitModel()
        {

        }
        #endregion
        #region 代理Model管理Proxy的功能
        /// <summary>
        /// 是否有Proxy
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        public bool HasProxy(string proxyName)
        {
            return model.HasProxy( proxyName );
        }
        /// <summary>
        ///  注册Proxy
        /// </summary>
        /// <param name="proxy"></param>
        public void RegisterProxy(IProxy proxy)
        {
            model.RegisterProxy( proxy );
        }
        /// <summary>
        /// 移除Proxy
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        public IProxy RemoveProxy(string proxyName)
        {
            return model.RemoveProxy( proxyName );
        }
        /// <summary>
        /// 获取Proxy
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        public IProxy RetrieveProxy(string proxyName)
        {
            return model.RetrieveProxy( proxyName );
        }


        #endregion
        #region 代理View管理Mediaotor

        
        public void RegisterMediator(IMediator mediator)
        {
            view.RegisterMediator( mediator );
        }

        public IMediator RetrieveMediator(string mediatorName)
        {
            return view.RetrieveMediator( mediatorName );
        }

        public IMediator RemoveMediator(string mediatorName)
        {
            return view.RemoveMediator( mediatorName );
        }

        public bool HasMediator(string mediatorName)
        {
            return view.HasMediator( mediatorName );
        }
        #endregion
        #region 发送消息
        public void SendNotification(string name, object data = null)
        {
            NotificationCenter.GetInstance().SendNotification( name, data );
        }
        #endregion

    }
}
