using UnityEngine;
using System.Collections;
using TarenaMVC.Patterns;
using System;

namespace TarenaMVC.Tests
{
    public class MainMediator : IObserver
    {
        public void HandleNotification(Notification notification)
        {
            Debug.Log( "MainMediator::HandleNotification " + notification.name );
        }
    }
}

