using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
	/// <summary>
	/// 影响
	/// </summary>
	public interface IImpact
    { 
        //影响
        void DoImpact(SkillDeployer deployer);
	}
}