using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace TRPG.MVC
{
    public class HeroProxy : BaseProxy
    {
        public new const string NAME = "HeroProxy";
        public HeroProxy()
        {
            this.proxyName = NAME;
        }
        /// <summary>
        ///  获取用户英雄
        /// </summary>
        public void GetUserHero()
        {
            Debug.Log( "HeroProxy GetUserHero" );
            // 打开数据库
            OpenDB();
            // 查询
            string uid = GameController.instance.crtUser.uid;
            SqliteDataReader reader = db.Select( "User_Hero", "UserId", GetStr(uid) );
            if( reader.HasRows ) // 读取数据
            {
                ArrayList list = new ArrayList();
                while(reader.Read())
                {
                    UserHeroVO userHero = new UserHeroVO();
                    userHero.heroName = reader.GetString( reader.GetOrdinal( "Name" ) );
                    userHero.level = reader.GetInt32( reader.GetOrdinal( "Lv" ) );
                    userHero.exp = reader.GetInt32( reader.GetOrdinal( "Exp" ) );
                    userHero.money = reader.GetInt32( reader.GetOrdinal( "Money" ) );
                    userHero.force = reader.GetInt32( reader.GetOrdinal( "Force" ) );
                    list.Add( userHero );
                    Debug.Log( userHero.heroName );
                }
                SendNotification( NotificationList.GET_USER_HERO + SUCCESS, list );
            }
            else
            {
                // 当前用户没有英雄,直接打开创建英雄面板新建英雄,而且此操作不能取消
                SendNotification( NotificationList.GET_USER_HERO + SUCCESS, "cantCancel" );
            }
            // 关闭数据库
            CloseDB();
        }

        /// <summary>
        /// 获取系统英雄列表
        /// </summary>
        public void GetSystemHeroList()
        {
            // 打开数据库
            OpenDB();
            // 获取数据
            SqliteDataReader reader = db.ReadFullTable( "Hero" );
            Dictionary<string, HeroVO> heros = new Dictionary<string, HeroVO>();
            while( reader.Read())
            {
                HeroVO hero = new HeroVO();
                hero.id = reader.GetInt32( reader.GetOrdinal( "id" ) ) + "";
                hero.uid = reader.GetString( reader.GetOrdinal( "uid" ) );
                hero.name = reader.GetString( reader.GetOrdinal( "name" ) );
                hero.type = reader.GetString( reader.GetOrdinal( "type" ) );
                hero.role = reader.GetString( reader.GetOrdinal( "role" ) );
                hero.weapon = reader.GetString( reader.GetOrdinal( "weapon" ) );
                hero.description = reader.GetString( reader.GetOrdinal( "description" ) );

                heros.Add( hero.type, hero );
            }
            Debug.Log( "GET_SYSTEM_HERO_LIST " + heros );
            SendNotification( NotificationList.GET_SYSTEN_HERO + SUCCESS, heros );
            // 关闭数据库
            CloseDB();
        }

        /// <summary>
        /// 创建新英雄
        /// </summary>
        /// <param name="userHero"></param>
        public void CreateHero( UserHeroVO userHero)
        {
            // 打开数据库
            OpenDB();
            // 获取数据
            SqliteDataReader reader = db.Select( "User_Hero", "name", GetStr( userHero.heroName ) );
            if( reader.HasRows) // 有重名
            {
                SendNotification( NotificationList.CREATE_HERO + FAILURE, "英雄已存在!" );
                CloseDB();return;
            }
            db.InsertInto( "User_Hero", userHero.GetString() );
            SendNotification( NotificationList.CREATE_HERO + SUCCESS );
            // 关闭数据库
            CloseDB();
        }


    }
}

