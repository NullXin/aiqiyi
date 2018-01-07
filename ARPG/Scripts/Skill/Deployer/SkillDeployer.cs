using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
    ///<summary>
    /// 技能释放器：附加到技能预制件上，用于定义所有释放器共有行为。
    ///</summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        //当前技能
        private SkillData currentSkillData;

        public SkillData CurrentSkillData
        {
            get
            { return currentSkillData; }
            set
            {
                currentSkillData = value;
                InitDeployer();//创建算法对象
            }
        }

        //选区算法对象
        private IAttackSelector selector;

        //影响算法对象
        private List<IImpact> impactList;

        //初始化释放器:创建释放器依赖的算法对象
        private void InitDeployer()
        {
            //……
            //currentSkillData.selectorType.ToString()  -->  Sector
            //命名空间.类名
            //ARPGDemo.Skill. + 枚举 + AttackSelector
            selector= DeployerConfigFactory.CreateAttackSelector(currentSkillData);
            //currentSkillData.impactType
            //命名空间.类名
            //ARPGDemo.Skill. + 影响名称 + Impact
            impactList = DeployerConfigFactory.CreateAttackImpact(currentSkillData);
        }

        /// <summary>
        /// 计算攻击目标
        /// </summary>
        public void CalculateTargets()
        {
            //通过选区计算目标
            currentSkillData.attackTargets = selector.SelectTarget(currentSkillData, transform);
        }

        /// <summary>
        /// 影响目标
        /// </summary>
        public void ImpactTarget()
        {
            //遍历影响算法对象
            for(int i=0;i< impactList.Count; i++)
            {
                impactList[i].DoImpact(this);
            }
        }

        /// <summary>
        /// 回收技能
        /// </summary>
        public void CollectSkill()
        {
            GameObjectPool.Instance.CollectObject(gameObject, currentSkillData.durationTime);
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        public abstract void DeployerSkill();
    }
}