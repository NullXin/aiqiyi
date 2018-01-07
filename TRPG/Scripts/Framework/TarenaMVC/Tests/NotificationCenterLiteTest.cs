using UnityEngine;
using System.Collections;
using TarenaMVC.Patterns;

namespace TarenaMVC.Tests
{
    public class NotificationCenterLiteTest : MonoBehaviour
    {
        void Start()
        {
            MainMediator mainMediator = new MainMediator();
            UserMediator userMediator = new UserMediator();

            NotificationCenterLite ncLite = new NotificationCenterLite();
            ncLite.AddObserver( mainMediator, "LoginSuccess" );
            ncLite.ViewObserver();
            ncLite.AddObserver( userMediator, "LoginSuccess" );
            ncLite.AddObserver( userMediator, "Login" );
            ncLite.AddObserver( userMediator, "Register" );
            ncLite.ViewObserver();
            ncLite.RemoveObserver( userMediator, "LoginSuccess" );
            ncLite.ViewObserver();

            ncLite.SendNotification( "Login" );
        }
    }
}

