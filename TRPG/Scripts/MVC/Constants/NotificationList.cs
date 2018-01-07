using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRPG.MVC
{
	///<summary>
	///
	///</summary>
	public class NotificationList
	{
        // 用户系统消息
        public const string LOGIN = "Login";
        public const string REGISTER = "Register";
        public const string LOGOUT = "Logout";
       
        // 主界面
        public const string SHOW_PANEL = "ShowPanel";
        public const string PLAY_LEVEL = "PlayLevel";
        public const string LEVEL_LOADED = "LevelLoaded";
        public const string UNLOAD_LEVEL = "UnloadLevel";
        // 角色系统(英雄)
        public const string GET_USER_HERO = "GetUserHero";
        public const string HERO_STAGE = "HeroStage";
        public const string GET_SYSTEN_HERO = "GetSystemHero";
        public const string CHANGE_SYSTEM_HERO = "ChangeSystemHero";
        public const string CREATE_HERO = "CreateHero";


        // 结果
        public const string SUCCESS = "Success";
        public const string FAILURE = "Failure";
        // 显示和隐藏
        public const string SHOW = "Show";
        public const string HIDE = "Hide";
    }
}