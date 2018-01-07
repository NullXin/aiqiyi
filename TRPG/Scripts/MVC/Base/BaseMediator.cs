using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TarenaMVC.Patterns;

namespace TRPG.MVC
{
    /// <summary>
    /// 封装获取facade和proxy的方法
    /// </summary>
    public class BaseMediator:Mediator
    {
        public new static string NAME = "BaseMediator";

        // 结果
        public const string SUCCESS = "Success";
        public const string FAILURE = "Failure";
        // 显示和隐藏
        public const string SHOW = "Show";
        public const string HIDE = "Hide";
        public BaseMediator()
        {
            this.mediatorName = NAME;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public override void SendNotification(string name, object data = null)
        {
            facade.SendNotification( name, data );
        }
        /// <summary>
        /// 获取facade
        /// </summary>
        protected ApplicationFacade facade
        {
            get { return ApplicationFacade.GetInstance(); }
        }
        /// <summary>
        /// 获取userProxy
        /// </summary>
        protected UserProxy userProxy
        {
            get { return facade.RetrieveProxy( UserProxy.NAME ) as UserProxy;  }
        }

        protected HeroProxy heroProxy
        {
            get { return facade.RetrieveProxy( HeroProxy.NAME ) as HeroProxy; }
        }
    }
}
