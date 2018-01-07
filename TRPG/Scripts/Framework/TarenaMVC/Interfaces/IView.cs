using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TarenaMVC.Interfaces
{
    public interface IView
    {
        /// <summary>
        /// 注册Mediator
        /// </summary>
        /// <param name="mediator"></param>
        void RegisterMediator(IMediator mediator);
        /// <summary>
        /// 获取Mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        IMediator RetrieveMediator(string mediatorName);
        /// <summary>
        /// 移除Mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        IMediator RemoveMediator(string mediatorName);
        /// <summary>
        /// 有无Mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        bool HasMediator(string mediatorName);
    }
}
