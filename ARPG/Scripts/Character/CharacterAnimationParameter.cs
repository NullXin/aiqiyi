using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
	///<summary>
	/// 角色动画参数类
	///</summary>
    [System.Serializable]//可序列化：可以在Unity编译器中显示数据成员
	public class CharacterAnimationParameter
	{
        public string Run = "run";

        public string Death = "death";

        public string Idle = "idle";

        public string Attack01 = "Attack1";

        public string Attack02 = "Attack2";
         
        public string Attack03 = "Attack3";

        public string Walk = "Walk";
    }
}