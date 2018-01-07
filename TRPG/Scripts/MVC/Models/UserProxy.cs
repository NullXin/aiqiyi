using UnityEngine;
using System.Collections;
using TarenaMVC.Patterns;
using Mono.Data.Sqlite;
using System;
namespace TRPG.MVC
{
    public class UserProxy : BaseProxy
    {
        public new static string NAME = "UserProxy";
        public UserProxy(object data = null)
        {
            this.proxyName = NAME;
            this.data = data;
        }
        /// <summary>
        /// 登录
        /// </summary>
        public void Login( UserVO user )
        {
            OpenDB();
            SqliteDataReader reader = db.SelectWhere( "User",
                new string[] { "uid", "username" },
                new string[] {"username", "password"},
                new string[] {"=","="},
                new string[] { user.username,user.password}
            );
            if (reader.HasRows)
            {
               
                // 保存当前用户数据
                reader.Read();
                string uid = reader.GetString( reader.GetOrdinal( "uid" ) );
                user.uid = uid;
                GameController.instance.crtUser = user;

                Debug.Log( "用户验证成功,可以登录!" );
                SendNotification( NotificationList.LOGIN + NotificationList.SUCCESS, null );
            }
            else
            {
                Debug.Log( "用户或密码错误!" );
                SendNotification( NotificationList.LOGIN + NotificationList.FAILURE, "用户或密码错误!" );
            }
                
            CloseDB();
        }
        /// <summary>
        ///  注册
        /// </summary>
        public void Register(UserVO user)
        {
            // 打开数据库
            OpenDB();
            // 查找是否重名
            SqliteDataReader reader = db.Select( "User", "username", GetStr( user.username ) );
            if (reader.HasRows) // 用户已存在
            {
                SendNotification( NotificationList.REGISTER + FAILURE, "用户名已存在!" );
                CloseDB(); return;
            }
            // 插入数据库
            string uid = Guid.NewGuid().ToString( "N" );
            db.InsertInto( "User", 
                new string[] {
                    GetStr( uid ),
                    GetStr( user.username), 
                    GetStr( user.password ), 
                    GetStr( DateTime.Now ) 
                } );
            SendNotification( NotificationList.REGISTER + SUCCESS );
            // 关闭数据库
            CloseDB();
        }

    }
}

