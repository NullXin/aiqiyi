using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
	/// <summary>
	/// 近身释放器
	/// </summary>
	public class MeleeSkillDeployer : SkillDeployer
	{   
        /// <summary>
        /// 释放技能
        /// </summary>
        public override void DeployerSkill()
        {
            //计算目标
            CalculateTargets();
            //影响物体
            ImpactTarget();
            //回收技能
            CollectSkill();
        }
    }
}