using UnityEngine;
using System.Collections;
namespace wzx
{
    [System.Serializable]
    public class EnemyAnimationParameter 
    {
        /// <summary>
        /// 跑步动画名称
        /// </summary>
        public string run = "Run";
        /// <summary>
        /// 攻击动画名称
        /// </summary>
        public string attack = "Attack";
        /// <summary>
        /// 死亡动画名称
        /// </summary>
        public string death = "Death";
        /// <summary>
        /// 闲置动画名称
        /// </summary>
        public string idle = "Idle"; 
    }
}