using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Skill
{
	/// <summary>
	/// 选区
	/// </summary>
	public interface IAttackSelector
	{
        /// <summary>
        /// 范围目标
        /// </summary>
        /// <param name="data">技能数据，包含了范围等信息</param>
        /// <param name="skillTF">技能预制件所在变化组件的具体位置，查找</param>
        /// <returns></returns>
        /// 
        //SectorAttackSelector
        Transform[] SelectTarget(SkillData data,Transform skillTF);
    }
}