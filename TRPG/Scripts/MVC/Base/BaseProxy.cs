using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarenaMVC.Patterns;
using System.IO;
using System;

namespace TRPG.MVC
{
	///<summary>
	/// 封装数据库访问功能
	///</summary>
	public class BaseProxy:Proxy,INotifier
	{
        // 结果
        public const string SUCCESS = "Success";
        public const string FAILURE = "Failure";
        // 显示和隐藏
        public const string SHOW = "Show";
        public const string HIDE = "Hide";

        protected DbAccess db;
        private string dbPath;
        private string dbName = "data.db"; // 数据库名称

        /// <summary>
        ///  打开数据库
        /// </summary>
        protected void OpenDB()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN // unity编辑器 或 PC版本
            dbPath = Application.streamingAssetsPath + "/" + dbName;
#elif UNITY_ANDROID || UNITY_IOS // android 或 iOS版本
                dbPath = Application.persistentDataPath + "/"+ dbName;
             if( !File.Exists( dbPath ) ) // 如果数据库不存在
            {
                // 则copy 数据库
                GameController.instance.StartCoroutine( CopyDB() );
            }
#endif
            db = new DbAccess( "URI=file:" + dbPath );
        }
        /// <summary>
        /// 复制数据库: 将数据库文件从StreamingAssets目录复制到persistentDataPath
        /// </summary>
        /// <returns></returns>
        private IEnumerator CopyDB()
        {
            WWW www = new WWW(
                Application.streamingAssetsPath + "/" + dbName); // 使用WWW下载文件
            yield return www;  // 等待下载完毕
            File.WriteAllBytes( dbPath, www.bytes );  // 写入文件到persistentDataPath目录里面
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        protected void CloseDB()
        {
            db.CloseSqlConnection();
        }
        /// <summary>
        /// 对象前后添加单引号
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetStr( object o)
        {
            return "'" + o + "'";
        }
        public void SendNotification(string name, object data=null)
        {
            NotificationCenter.GetInstance().SendNotification( name, data );
        }
    }
}