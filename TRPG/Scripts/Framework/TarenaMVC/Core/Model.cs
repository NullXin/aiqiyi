using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarenaMVC.Interfaces;
using System;

namespace TarenaMVC.Core
{
    ///<summary>
    ///
    ///</summary>
    public class Model : IModel
    {

        private static Model instance;
        public static Model GetInstance()
        {
            if( instance == null)
            {
                instance = new Model();
            }
            return instance;
        }

        public Model()
        {
            if (instance != null)
                throw new Exception( "请使用GetInstance()方法获取实例" );
            instance = this;
        }
        /// <summary>
        /// 以proxyName为键名存储所有的Proxy
        /// </summary>
        Dictionary<string, IProxy> proxyMap = new Dictionary<string, IProxy>();
        /// <summary>
        /// 注册Proxy
        /// </summary>
        /// <param name="proxy"></param>
        public void RegisterProxy(IProxy proxy)
        {
            proxyMap[proxy.proxyName] = proxy;
            proxy.OnRegister();
        }  
        /// <summary>
        /// 通过proxyName获取proxy
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        public IProxy RetrieveProxy(string proxyName)
        {
            IProxy proxy = HasProxy(proxyName) ? proxyMap[proxyName] : null ;
            return proxy;
        }
        /// <summary>
        /// 移除Proxy
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        public IProxy RemoveProxy(string proxyName)
        {
            IProxy proxy = HasProxy( proxyName ) ? proxyMap[proxyName] : null;
            if( proxy != null)
            {
                proxy.OnRemove();
                proxyMap[proxyName] = null;
            }
            return proxy;
        }
        /// <summary>
        /// 判断有无某个Proxy
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        public bool HasProxy( string proxyName )
        {
            return proxyMap.ContainsKey( proxyName );
        }
    }
}