using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TarenaMVC.Patterns;

namespace TRPG.MVC
{
	///<summary>
	///
	///</summary>
	public class LoginPanel:BasePanel
	{

        private InputField usernameInput;
        private InputField pwdInput;
        private Button loginButton;
        private Button registerButton;

        void Awake()
        {           
            usernameInput = transform.Find( "usernameInput" ).GetComponent<InputField>();
            pwdInput = transform.Find( "pwdInput" ).GetComponent<InputField>();
            loginButton = transform.Find( "loginButton" ).GetComponent<Button>();
            registerButton = transform.Find( "registerButton" ).GetComponent<Button>();
            loginButton.onClick.AddListener( Login );
            registerButton.onClick.AddListener( Register );
        }

        void Login()
        {
            print( "Login" );
            if(usernameInput.text == "" || pwdInput.text == "")
            {
                Debug.LogError( "用户名和密码不能为空!!!" );
                return;
            }
            UserVO userVO = new UserVO();
            userVO.username = usernameInput.text;
            userVO.password = pwdInput.text;
            SendNotification( NotificationList.LOGIN, userVO );
        }

        void Register()
        {
            print( "ShowRegister" );
            SendNotification( SHOW + NotificationList.REGISTER );
        }



        // 点击LoginButton时获取用户账号和密码
    }
}