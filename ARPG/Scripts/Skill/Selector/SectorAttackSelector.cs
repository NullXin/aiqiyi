using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPGDemo.Character;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 扇形攻击
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        public Transform[] SelectTarget(SkillData data, Transform skillTF) {
            var targets = skillTF.CalculateAroundObject(data.attackDistance, data.attackAngle, data.attackTargetTags);
            //筛选活着的敌人
            targets = targets.FindAll(t => t.GetComponent<CharacterStatus>().HP > 0);
            if (targets.Length==0)
            {
                return null;
            }
            //获取单个敌人
            if (data.attackType == SkillAttackType.Single)
            {
                targets= new Transform[] { targets.GetMin(t => Vector3.Distance(t.position, skillTF.position)) };
            }
            return targets;
        }
    }
}