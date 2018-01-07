using System;
using System.Collections.Generic;
using TarenaMVC.Patterns;
using UnityEngine;

namespace TRPG.MVC
{
    public class MainMediator:BaseMediator
    {
        public new const string NAME = "MainMediator"; 
        public MainMediator()
        {
            this.mediatorName = NAME;
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                NotificationList.LOGIN + SUCCESS, // 登录成功
                NotificationList.LOGOUT, // 注销
                NotificationList.SHOW_PANEL, // 显示面板
                NotificationList.PLAY_LEVEL, // 加载场景
                NotificationList.LEVEL_LOADED, // 加载完成
                NotificationList.UNLOAD_LEVEL // 卸载场景
            };
        }

        public override void HandleNotification(Notification notification)
        {
            switch( notification.name)
            {
                case NotificationList.LOGIN + SUCCESS: // 登录成功
                    ToggleMainRelativeObjects( true );
                    break;
                case NotificationList.LOGOUT: // 注销
                    ToggleMainRelativeObjects( false );
                    break;
                case NotificationList.SHOW_PANEL: // 显示面板
                    ShowPanel( notification.data + "");
                    break;
                case NotificationList.PLAY_LEVEL: // 加载场景
                    loader.LoadScene( notification.data + "" );
                    break;
                case NotificationList.LEVEL_LOADED: // 加载完成
                    ToggleGameRelativeObjects( false );
                    HidePanel( "MapPanel" );
                    break;
                case NotificationList.UNLOAD_LEVEL: // 卸载场景
                    loader.UnloadLevel( notification.data + "" );
                    ToggleGameRelativeObjects( true );
                    break;
                default:
                    Debug.LogError( "未处理的消息: " + notification.name );
                    break;
            }
        }
        /// <summary>
        ///  显示面板
        /// </summary>
        /// <param name="v"></param>
        private void ShowPanel(string name)
        {
            BasePanel panel = GameController.GetChild( name ).GetComponent<BasePanel>();
            panel.Show();
        }
        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="name"></param>
        private void HidePanel(string name)
        {
            BasePanel panel = GameController.GetChild( name ).GetComponent<BasePanel>();
            panel.Hide();
        }

        /// <summary>
        /// 显示或隐藏主界面相关对象
        /// </summary>
        /// <param name="enable"></param>
        private void ToggleMainRelativeObjects( bool enable)
        {
            foreach (GameObject go in GameController.instance.mainRelativeObjects)
                go.SetActive( enable );
        }
        /// <summary>
        /// 显示或隐藏游戏相关对象
        /// </summary>
        /// <param name="enable"></param>
        private void ToggleGameRelativeObjects( bool enable)
        {
            foreach (GameObject go in GameController.instance.gameRelativeObjects)
                go.SetActive( enable );
        }
        /// <summary>
        /// 场景加载器
        /// </summary>
        private SceneLoader _loader;
        protected SceneLoader loader
        {
            get
            {
                if (_loader == null)
                    _loader = GameObject.FindObjectOfType<SceneLoader>();
                return _loader;
            }
        }

    }
}
