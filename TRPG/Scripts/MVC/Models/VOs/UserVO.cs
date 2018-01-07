using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRPG.MVC
{
	///<summary>
	///
	///</summary>
	public class UserVO
	{
        public string username;
        public string password;
        public string uid;

        public UserVO() { }
        public UserVO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
	}
}