using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;
using Common;

namespace ARPGDemo.Skill
{
	/// <summary>
	/// 减血
	/// </summary>
	public class DamageImpact : IImpact
    {
       
        public void DoImpact(SkillDeployer deployer)
        {
            //攻击
            deployer.StartCoroutine(RepeatAttack(deployer));
        }

        /// <summary>
        /// 攻击
        /// </summary>
        private void Attack(SkillData data)
        {
            //计算攻击值
            float atk = data.owner.GetComponent<CharacterStatus>().baseATK * data.atkRatio;
            //遍历所有攻击目标
            for(int i=0;i< data.attackTargets.Length; i++)
            {
                //创建攻击粒子系统
                var hitFXObject = GameObjectPool.Instance.CreateObject("hitFX",
                    data.hitFxPrefab,data.attackTargets[i].FindChildByName("HitFxPos").position,
                    Quaternion.identity);
                data.attackTargets[i].GetComponent<CharacterStatus>().Damage(atk);
                GameObjectPool.Instance.CollectObject(hitFXObject, 2);
            }
        }

        /// <summary>
        /// 重复攻击
        /// </summary>
        /// <returns></returns>
        private IEnumerator RepeatAttack(SkillDeployer deployer)
        {
            float atkTime = 0;
            do
            {
                if(deployer.CurrentSkillData.attackTargets!=null) Attack(deployer.CurrentSkillData);
                yield return new WaitForSeconds(deployer.CurrentSkillData.atkInterval);
                atkTime += deployer.CurrentSkillData.atkInterval;
                //再次计算攻击目标
                deployer.CalculateTargets();
            } while (atkTime< deployer.CurrentSkillData.durationTime);
        }
    }
}