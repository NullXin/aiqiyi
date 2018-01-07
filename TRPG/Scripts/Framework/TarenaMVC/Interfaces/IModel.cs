using System;
using System.Collections.Generic;


namespace TarenaMVC.Interfaces
{
    public interface IModel
    {
        // 注册IProxy
        void RegisterProxy(IProxy proxy);
        // 获取IProxy
        IProxy RetrieveProxy(string proxyName);
        // 删除IProxy
        IProxy RemoveProxy(string proxyName);
        // 有无IProxy
        bool HasProxy(string proxyName);
    }
}
