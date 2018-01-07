using UnityEngine;
using System.Collections;
using System;

namespace wzx
{
    /// <summary>
    /// 主角状态类
    /// </summary>
    public class PlayerStatus : CharacterStatus
    {  
        public static PlayerStatus Instance { get; private set; }
        public Transform headTF; //玩家头部受伤点
        
        public Color demageColor;//玩家受伤害的颜色
        public Color originalColor;//玩家最终受伤害的颜色
        private Vector3 startPosition;//开始位置
        private Quaternion startRotation;//开始方向


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            originalColor = demageColor;
        }

        /// <summary>
        /// 玩家受伤害
        /// </summary>
        protected override void OnDamage()
        {
            if (HP<=maxHP/4)
            {
                demageColor.a = 0.7f;
            }
           DamageScreenMask.Instance.DamageScreenEffect();
        }

        /// <summary>
        /// 玩家死亡
        /// </summary>
        protected override void Death()
        {
            base.Death();
            demageColor = originalColor;
            GameController.Instance.GameOver();
        }

        /// <summary>
        /// 玩家重置位置
        /// </summary>
        public void ResetPosition()
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}