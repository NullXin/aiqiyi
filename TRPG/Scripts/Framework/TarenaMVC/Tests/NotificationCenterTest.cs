using UnityEngine;
using System.Collections;
using TarenaMVC.Patterns;
using System;

namespace TarenaMVC.Tests
{
    public class NotificationCenterTest : MonoBehaviour
    {
        public void HandleNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        // Use this for initialization
        void Start()
        {
            MainMediator mainMediator = new MainMediator();
            UserMediator userMediator = new UserMediator();

            NotificationCenter nc = NotificationCenter.GetInstance();
            // 测试AddObserver
            nc.AddObserver( mainMediator, "ShowSkillPanel" );
            nc.AddObserver( userMediator, "LoginSucess" );
            nc.AddObserver( userMediator, "Register" );
            nc.AddObserver( mainMediator, "LoginSucess" );
            // 查看Observers
            nc.ViewObserver();
            // 测试RemoveObserver
            //nc.RemoveObserver( userMediator , "LoginSucess" );
            //nc.RemoveObserver( mainMediator, "LoginSucess" );
            // 查看Observers
            return;
            nc.ViewObserver();
            print( "SendNotification( Register  )" );
            nc.SendNotification( "Register2" );
            print( "SendNotification( LoginSucess)" );
            nc.SendNotification( "LoginSucess" );
            mainMediator = null;
            nc.RemoveObserver( userMediator, "LoginSucess" );
            nc.ViewObserver();
            print( "SendNotification( LoginSucess)" );
            nc.SendNotification( "LoginSucess" );

        }

    }
}

