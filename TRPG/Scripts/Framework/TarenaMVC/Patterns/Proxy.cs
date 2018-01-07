using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TarenaMVC.Interfaces;

namespace TarenaMVC.Patterns
{
    public class Proxy : IProxy
    {
        public static string NAME = "Proxy";

        public Proxy( object data = null)
        {
            this.proxyName = NAME;
            this.data = data;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string proxyName{ get;  set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// Model在注册Proxy时调用
        /// </summary>
        public virtual void OnRegister(){ }
        /// <summary>
        /// Model在移除Proxy时调用
        /// </summary>
        public virtual void OnRemove() { } 
    }
}
