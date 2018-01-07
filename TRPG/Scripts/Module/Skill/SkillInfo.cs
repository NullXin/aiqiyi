using UnityEngine;
using System.Collections;

namespace TRPG.Module
{
    /// <summary>
    /// 技能信息
    /// </summary>
    public class SkillInfo : ScriptableObject
    {
        // 基本信息
        public string name; // 技能名称
        public AnimationClip animation; // 技能释放动画
        public string description; // 技能详细信息
        public Sprite icon; // 技能icon
        // 伤害
        public float range = 2; // 攻击范围
        public float damage = 50; // 正常伤害值
        public LayerMask attackTarget; // 攻击对象
        public float damageDelay = 0.2f;// 动画播放多久时候计算伤害值
        // 效果
        public GameObject startEffect; // 技能开始效果
        public GameObject hitEffect; // 技能打击效果
        public float hitEffectDelay; // 技能打击效果时间延迟

        public override string ToString()
        {
            return "技能名称: " + name + "\n攻击范围:" + range+ "\n伤害值:" + damage + "\n技能详细信息:" + description;
        }
    }
}

