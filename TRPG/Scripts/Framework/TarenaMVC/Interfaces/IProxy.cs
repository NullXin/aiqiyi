using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarenaMVC.Interfaces
{
	///<summary>
	/// IProxy
	///</summary>
	public interface IProxy
	{
        /// <summary>
        /// 名称
        /// </summary>
        string proxyName { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        object data { get; set; }
        /// <summary>
        /// Model在注册Proxy时调用
        /// </summary>
        void OnRegister();
        /// <summary>
        /// Model在移除Proxy时调用
        /// </summary>
        void OnRemove();
	}
}