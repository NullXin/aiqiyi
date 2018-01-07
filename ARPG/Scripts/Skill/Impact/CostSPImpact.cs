using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;
namespace ARPGDemo.Skill
{
    /// <summary>
    /// 消费法力
    /// </summary>
    public class CostSPImpact : IImpact
    {
        public void DoImpact(SkillDeployer deployer)
        {
            var chState = deployer.CurrentSkillData.owner.GetComponent<CharacterStatus>();
            chState.SP -= deployer.CurrentSkillData.costSP;
        }
    }
}