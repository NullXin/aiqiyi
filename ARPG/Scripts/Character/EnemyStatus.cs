using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    ///<summary>
    /// 敌人状态类
    ///</summary>
    public class EnemyStatus : CharacterStatus
    { 
        public override void Death()
        {
            base.Death();
            Destroy(gameObject, 5);
        }
    }
}