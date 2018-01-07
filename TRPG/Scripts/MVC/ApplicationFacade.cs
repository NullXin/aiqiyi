using UnityEngine;
using System.Collections;
using TarenaMVC.Core;

namespace TRPG.MVC
{
    /// <summary>
    /// MVC框架的入口
    /// </summary>
    public class ApplicationFacade:Facade
    {
        public static ApplicationFacade GetInstance()
        {
            if (instance == null) instance = new ApplicationFacade();
            return instance as ApplicationFacade;
        }
        /// <summary>
        /// 初始化Model
        /// 在这里注册所有的Proxy
        /// </summary>
        protected override void InitModel()
        {
            RegisterProxy( new UserProxy() );
            RegisterProxy( new HeroProxy() );
        }
        /// <summary>
        /// 启动MVC
        /// 在这里注册所有的Mediator
        /// </summary>
        public void Start()
        {
            Debug.Log( "ApplicationFacade Start" );
            //UserProxy userProxy = RetrieveProxy( UserProxy.NAME ) as UserProxy;
            //Debug.Log( userProxy );

            RegisterMediator( new UserMediator() );
            RegisterMediator( new MainMediator() );
            RegisterMediator( new HeroMediator() );
        }

    }
}

