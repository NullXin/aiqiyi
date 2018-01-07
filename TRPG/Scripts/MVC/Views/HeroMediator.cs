using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TarenaMVC.Patterns;

namespace TRPG.MVC
{
    public class HeroMediator : BaseMediator
    {
        public new const string NAME = "HeroMediator";
        public HeroMediator()
        {
            this.mediatorName = NAME;
        }

        public override string[] ListNotificationInterests()
        {
            return new string[] {
                   NotificationList.GET_USER_HERO,
                   NotificationList.GET_USER_HERO + SUCCESS,
                   SHOW + NotificationList.HERO_STAGE,
                   NotificationList.GET_SYSTEN_HERO + SUCCESS,
                   NotificationList.CHANGE_SYSTEM_HERO,
                   NotificationList.CREATE_HERO,
                   NotificationList.CREATE_HERO + SUCCESS,
                   NotificationList.CREATE_HERO + FAILURE
            };
        }

        public override void HandleNotification(Notification notification)
        {
            switch(notification.name)
            {
                case NotificationList.GET_USER_HERO: // 获取用户英雄
                    heroProxy.GetUserHero();
                    break;
                case NotificationList.GET_USER_HERO + SUCCESS: // 获取用户英雄成功
                    ArrayList heroList = notification.data as ArrayList;
                    if (notification.data.ToString() == "cantCancel")
                        SendNotification( SHOW + NotificationList.HERO_STAGE, "cantCancel" );
                    if(heroList!= null ) heroPanel.UpdateUserHero( heroList );
                    break;
                case SHOW + NotificationList.HERO_STAGE: // 创建新英雄
                    heroPanel.Hide();
                    heroStage.Show();
                    heroStage.showCloseButton = notification.data.ToString() != "cantCancel";
                    heroProxy.GetSystemHeroList();
                    break;
                case NotificationList.GET_SYSTEN_HERO + SUCCESS: // 获取系统英雄成功

                    heroStage.heroList =  notification.data as Dictionary<string, HeroVO>;
                    break;
                case NotificationList.CHANGE_SYSTEM_HERO: // 切换系统英雄
                    heroStage.ChangeSystemHero( notification.data + "" );
                    break;
                case NotificationList.CREATE_HERO: // 创建英雄
                    heroProxy.CreateHero( notification.data as UserHeroVO );
                    break; 
                case NotificationList.CREATE_HERO + SUCCESS: // 创建英雄成功
                    heroStage.Hide();
                   
                    break;
                case NotificationList.CREATE_HERO + FAILURE: // 创建英雄失败
                    Debug.LogError( "创建英雄失败:" + notification.data );
                    break;
                default:
                    Debug.LogError( "未处理的消息:" + notification.name );
                    break;
            }
        }


        private HeroStage _heroStage;
        protected HeroStage heroStage
        {
            get
            {
                if (_heroStage == null)
                    _heroStage = GameController.GetChild( "HeroStage" ).GetComponent<HeroStage>();
                return _heroStage;
            }
        }

        private HeroPanel _heroPanel;
        protected HeroPanel heroPanel
        {
            get
            {
                if (_heroPanel == null)
                    _heroPanel = GameController.GetChild( "HeroPanel" ).GetComponent<HeroPanel>();
                return _heroPanel;
            }
        }

    }
}

