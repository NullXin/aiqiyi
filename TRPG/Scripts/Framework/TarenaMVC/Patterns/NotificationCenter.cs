using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarenaMVC.Patterns
{
	///<summary>
	/// 消息中心
    /// 负责消息的注册,接收和发送
	///</summary>
	public class NotificationCenter : MonoBehaviour,INotifier
	{

        #region  单例模式
        private static NotificationCenter instance;
        /// <summary>
        /// 获取单例方法
        /// </summary>
        /// <returns></returns>
        public static NotificationCenter GetInstance()
        {
            if( instance == null)
            {
                GameObject go = new GameObject( "NotificationCenter" );
                instance = go.AddComponent<NotificationCenter>();
                DontDestroyOnLoad( go ); // 一直存在在内存中
            }
            return instance;
        }

        #endregion
        #region 添加观察者
        /// <summary>
        /// 存放消息和对应的IObserver
        /// </summary>
        Dictionary<string, List<IObserver>> map = new Dictionary<string, List<IObserver>>();
        
        public void AddObserver( IObserver observer, string name)
        {            
            if( name == null || name == "")   // 处理name非法的情况
            {
                Debug.LogError( "消息名称不能为空!!!" );
                return;
            }         
            if( map.ContainsKey(name) == false)   // 查找有无对应List              
                map[name] = new List<IObserver>();  // 没有新建一个,放到map里面                   
            List<IObserver> observerList = map[name];  // 将Observer添加到list里面
            // 只有没有的才可以添加,禁止同一个IObserver对同一个消息重复添加
            if ( !observerList.Contains( observer ) )
                observerList.Add( observer );
        }
        /// <summary>
        /// 查看所有Observer
        /// </summary>
        public void ViewObserver()
        {
            string s = "----------------------  View Observer Start  --------------------------------------\n" ;
            int count = 0;
            foreach ( string key in map.Keys )
            {
                s += count + "::"+ key + " :[ ";
                List<IObserver> list = map[key];
                for( int i = 0 ; i<list.Count ; i++)
                {
                    s += list[i] ;
                    if (i != list.Count - 1) s += " , ";
                }
                s += " ]\n";
                count++;            
            }           
            s += "----------------------  View Observer End  ---------------------------------------\n\n\n\n\n";
            print( s );
        }
        #endregion
        #region 移除观察者
        public void RemoveObserver( IObserver observer, string name)
        {
            if ( !map.ContainsKey( name ) ) return; // 没有则直接返回
            List<IObserver> observerList = map[name];// 根据name获取observerList          
            if ( observerList.Contains( observer ) ) // 如果observer在list里面
                observerList.Remove( observer ); // 从list中移除observer
            if ( observerList.Count == 0 ) // 如果list已经被清空
                map.Remove( name ); // 从map里面remove key
            
        }
        #endregion
        #region 发送消息
        public void SendNotification(string name, object data=null)
        {
            Debug.Log( "SendNotification::  name " + name + " data " + data );
            if( name == null || name == "")
            {
                Debug.LogError( "消息名不能为空!" );  return;
            }
            
            if( !map.ContainsKey(name) )
            {
                Debug.LogError( "未监听的消息::" + name );  return;
            }
            List<IObserver> observerList = map[name];
            Notification n = new Notification( name, data );
            // 保证发送消息期间list不发生变化
            List<IObserver> cloneList = new List<IObserver>( observerList );
            List<IObserver> observersToRemove = new List<IObserver>(); // 需要移除的Observer
            foreach( IObserver observer in cloneList) // 遍历所有observer
            {
                if (observer == null)  // 如果已经为空,则有待移除
                    observersToRemove.Add( observer ); // 移除
                else  // 不为空,则执行HandleNotification方法
                    observer.HandleNotification( n );
            }
            // 移除所有为空的observer
            foreach (IObserver observer in observersToRemove)
                observerList.Remove( observer );
        }
        #endregion
    }
    /// <summary>
    /// 发送的消息
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// 消息名称
        /// </summary>
        public string name;
        /// <summary>
        /// 消息数据
        /// </summary>
        public object data;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public Notification( string name, object data = null )
        {
            this.name = name;
            this.data = data;
        }
    }
    /// <summary>
    /// 消息处理接口
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="notification"></param>
        void HandleNotification( Notification notification );
    }
    /// <summary>
    /// 消息发送接口
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        void SendNotification(string name, object data);
    }

}