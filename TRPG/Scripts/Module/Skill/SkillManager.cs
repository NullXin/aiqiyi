using UnityEngine;
using System.Collections;

namespace TRPG.Module
{
    [RequireComponent(typeof(Animation))]
    public class SkillManager : MonoBehaviour
    {
        private Animation animation;
        private void Awake()
        {
            animation = GetComponent<Animation>();
        }
        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="skill"></param>
        public void Fire( SkillInfo skill)
        {
            print( "释放技能:" + skill.name );
            animation.Play( skill.animation.name ); // 播放人物动画
            StartCoroutine( DoAttack( skill ) ); // 计算伤害值
            // 技能开始效果
            if( skill.startEffect != null)
            {
                GameObject startEffect = Instantiate( skill.startEffect, transform.position, transform.rotation );              
                Destroy( startEffect, 5 ); // 5秒后销毁startEffect
            }           
            // 技能打击效果
            if( skill.hitEffect != null ) StartCoroutine( DelayCreate( skill ) );
           
        }
        /// <summary>
        /// 伤害处理
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        IEnumerator DoAttack( SkillInfo skill)
        {
            yield return new WaitForSeconds( skill.damageDelay ); // 等待一会
            // 找到所有攻击范围之内攻击对象
            Collider[] colliders = Physics.OverlapSphere( transform.position, skill.range, skill.attackTarget );
            print( colliders.Length );
            foreach( Collider c in colliders)
            {
                // 计算距离
                float distance = Vector3.Distance( transform.position, c.transform.position );
                // 根据距离计算伤害值
                float damage = ( 1 - distance / skill.range ) * skill.damage;
                // 找到Health. 接收伤害
                Health h = c.gameObject.GetComponent<Health>();
                h.TakeDamage( damage );
            }

        }
        /// <summary>
        /// 延迟创建
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        IEnumerator DelayCreate( SkillInfo skill)
        {
            yield return new WaitForSeconds( skill.hitEffectDelay );
            GameObject hitEffect =  Instantiate( skill.hitEffect, transform.position, transform.rotation );
            Destroy( hitEffect, 5 );// 5秒后自动销毁
        }

        
    }
}

