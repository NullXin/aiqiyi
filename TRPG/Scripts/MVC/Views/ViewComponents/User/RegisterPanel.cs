using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace TRPG.MVC
{
    /// <summary>
    /// 注册面板
    /// </summary>
    public class RegisterPanel : BasePanel
    {
        public const string NAME = "RegisterPanel";

        private InputField usernameInput;
        private InputField pwdInput;
        private InputField pwdInput2;
        private Button returnButton;
        private Button registerButton;
        // Use this for initialization
        void Awake()
        {
            usernameInput = transform.Find( "usernameInput" ).GetComponent<InputField>();
            pwdInput = transform.Find( "pwdInput" ).GetComponent<InputField>();
            pwdInput2 = transform.Find( "pwdInput2" ).GetComponent<InputField>();
            returnButton = transform.Find( "returnButton" ).GetComponent<Button>();
            registerButton = transform.Find( "registerButton" ).GetComponent<Button>();
            returnButton.onClick.AddListener( ShowLogin );
            registerButton.onClick.AddListener( Register );
            // 自动隐藏
           
        }

        // Update is called once per frame
        void ShowLogin()
        {
            SendNotification( SHOW + NotificationList.LOGIN );
        }

        void Register()
        {
            if(usernameInput.text == "" || pwdInput.text == "" || pwdInput2.text == "")
            {
                Debug.LogError( "用户名和密码不能为空!!!" );
                return;
            }
            if( pwdInput.text != pwdInput2.text)
            {
                Debug.LogError( "密码和确认密码不一致!!!" );
                return;
            }
            if( StringHelper.CheckBadWord( usernameInput.text) || !StringHelper.IsSafeSqlString( usernameInput.text ) 
                || StringHelper.CheckBadWord( pwdInput.text ) || !StringHelper.IsSafeSqlString( pwdInput.text ))
            {
                Debug.LogError( "不允许非法字符" );
                return;
            }

            UserVO user = new UserVO( usernameInput.text, pwdInput.text );
            SendNotification( NotificationList.REGISTER, user );
        }
    }
}

