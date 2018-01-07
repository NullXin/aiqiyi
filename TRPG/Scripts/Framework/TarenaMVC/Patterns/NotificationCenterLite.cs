using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace TarenaMVC.Patterns
{
    public class NotificationCenterLite : INotifier
    {
        // 存放消息和对应的IObserver列表
        Dictionary<string, List<IObserver>> map = new Dictionary<string, List<IObserver>>();
        // 添加Observer
        public void AddObserver( IObserver observer, string name)
        {
            if (!map.ContainsKey( name ))
                map[name] = new List<IObserver>();
            map[name].Add( observer );
        }
        // 移除Oberver
        public void RemoveObserver( IObserver observer, string name)
        {
            if( map.ContainsKey(name ))
            {
                if (map[name].Contains( observer ))
                    map[name].Remove( observer );
            }
        }
        // 发送消息
        public void SendNotification(string name, object data=null)
        {
            if(map.ContainsKey( name ))
            {
                foreach (IObserver observer in map[name])
                    observer.HandleNotification( new Notification( name, data ) );
            }
        }
        /// <summary>
        /// 查看所有Observer
        /// </summary>
        public void ViewObserver()
        {
            string s = "----------------------  View Observer Start  --------------------------------------\n";
            int count = 0;
            foreach (string key in map.Keys)
            {
                s += count + "::" + key + " :[ ";
                List<IObserver> list = map[key];
                for (int i = 0 ; i < list.Count ; i++)
                {
                    s += list[i];
                    if (i != list.Count - 1) s += " , ";
                }
                s += " ]\n";
                count++;
            }
            s += "----------------------  View Observer End  ---------------------------------------\n\n\n\n\n";
            Debug.Log( s );
        }
    }
}

