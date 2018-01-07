using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
	///<summary>
	/// 角色状态
	///</summary>
	public class CharacterStatus : MonoBehaviour
	{
        public CharacterAnimationParameter animParams;

        public float attackDistance = 3;

        public float attackInterval = 1;

        public float baseATK = 100;

        public float defence = 100;

        public float HP = 100;

        public float maxHP = 100;

        public float maxSP = 100;

        public float SP = 100;

        protected Animator animator;
        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        /// <summary>
        /// 受伤害
        /// </summary>
        /// <param name="val"></param>
        public void Damage(float val)
        {
            //扣除防御力
            val -= defence;

            if (val > 0)
            {
                HP -= val;
                if (HP <= 0) Death();
            }
        }

        /// <summary>
        /// 播放死亡动画
        /// </summary>
        public virtual void Death()
        {
            GetComponentInChildren<Animator>().SetBool(animParams.Death, true);
        }

        /// <summary>
        /// 是否正在攻击
        /// </summary>
        /// <returns></returns>
        public bool IsAttacking()
        {
            return animator.GetBool(animParams.Attack01) || animator.GetBool(animParams.Attack02) || animator.GetBool(animParams.Attack03);
        }
    }
}