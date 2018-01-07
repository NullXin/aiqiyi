using UnityEngine;
using System.Collections;
using TarenaMVC.Patterns;
using System;

namespace TarenaMVC.Tests
{
    public class UserMediator : IObserver
    {
        public void HandleNotification(Notification notification)
        {
            Debug.Log( "UserMediator::HandleNotification " + notification.name );
        }
    }
}

