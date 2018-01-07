using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    ///<summary>
    /// 玩家状态类：存储玩家数据
    ///</summary>
    public class PlayerStatus : CharacterStatus
    { 
        public override  void Death()
        {
            base.Death();
            //游戏结束
            print("玩家死亡");
        }
    }
}