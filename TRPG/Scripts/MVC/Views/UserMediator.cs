using System;
using System.Collections.Generic;
using TarenaMVC.Patterns;
using UnityEngine;

namespace TRPG.MVC
{
    public class UserMediator:BaseMediator
    {
        public new static string NAME = "UserMediator";

        public UserMediator()
        {
            this.mediatorName = NAME;
        }
        /// <summary>
        /// 监听消息
        /// </summary>
        /// <returns></returns>
        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                NotificationList.LOGIN,
                NotificationList.LOGIN + SUCCESS,
                NotificationList.LOGIN + FAILURE,
                NotificationList.REGISTER,
                NotificationList.REGISTER + SUCCESS,
                NotificationList.REGISTER + FAILURE,              
                SHOW + NotificationList.LOGIN,
                SHOW + NotificationList.REGISTER,
                NotificationList.LOGOUT
            };
        }
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="notification"></param>
        public override void HandleNotification(Notification notification)
        {
            Debug.Log( "UserMediator HandleNotification:: " + notification.name + " data " + notification.data );
            switch ( notification.name)
            {
                case NotificationList.LOGIN:
                    // 调用UserProxy的Login()
                    userProxy.Login( notification.data as UserVO );
                    break;
                case NotificationList.LOGIN + SUCCESS: // 登录成功
                    ToggleUserRelativeObjects( false );
                    heroProxy.GetUserHero();
                    break;
                case NotificationList.LOGIN + FAILURE:
                    Debug.LogError( notification.data );
                    break;
                case NotificationList.REGISTER:
                    // 调用UserProxy的Register方法
                    userProxy.Register( notification.data as UserVO );
                    break;
                case NotificationList.REGISTER + SUCCESS:
                    // 注册成功,切换到登录面板
                    SendNotification( SHOW + NotificationList.LOGIN );
                    break;
                case NotificationList.REGISTER + FAILURE:
                    Debug.LogError( notification.data );
                    break;
                
                case SHOW + NotificationList.LOGIN:  // 显示登录面板,隐藏注册面板
                    loginPanel.Show();
                    registerPanel.Hide();
                    break;
                case SHOW + NotificationList.REGISTER: // 显示注册面板,隐藏登录面板
                    registerPanel.Show();
                    loginPanel.Hide();
                    break;
                case NotificationList.LOGOUT:
                    ToggleUserRelativeObjects( true );
                    registerPanel.Hide();
                    break;
            }
        }

        private void ToggleUserRelativeObjects( bool v)
        {
            foreach (GameObject go in GameController.instance.userRelativeObjects)
                go.SetActive( v );
            
        }


        private LoginPanel _loginPanel;
        protected LoginPanel loginPanel
        {
            get
            {
                if (_loginPanel == null)
                    _loginPanel = GameController.GetChild( "LoginPanel" ).GetComponent<LoginPanel>();
                return _loginPanel;
            }
        }

        private RegisterPanel _registerPanel;
        protected RegisterPanel registerPanel
        {
            get
            {
                if (_registerPanel == null)
                    _registerPanel = GameController.GetChild( RegisterPanel.NAME ).GetComponent<RegisterPanel>();
                return _registerPanel;
            }
        }
    }
}
