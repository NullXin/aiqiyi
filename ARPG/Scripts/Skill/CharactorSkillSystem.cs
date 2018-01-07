using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using ARPGDemo.Character;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 角色技能系统：为攻击方提供简单释放技能方法
    /// </summary>

    [RequireComponent(typeof(CharacterSkillManager))]
    public class CharactorSkillSystem : MonoBehaviour
    {
        /*
            需求：
            1. 通过动画调用生成技能方法。
            2. 如果普攻，朝向目标。
            3. 选中目标，等待指定时间自动取消选中。
            4. 选中其他目标时，先取消上一个选中目标。                                      
        */

        //角色技能管理
        private CharacterSkillManager skillManager;
        //动画控制器
        private Animator anim;

        //保存数据
        private SkillData attackSkill;

        //攻击目标
        private Transform attackTargetTF;

        //队列保存释放技能
        private Queue<SkillData> queue=new Queue<SkillData>();
        
        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            anim = GetComponentInChildren<Animator>();
            //添加事件回调
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DeploySkill;
        }

        //释放技能
        private void DeploySkill()
        {
            //skillManager.GenerateSkill(queue.Dequeue());

            skillManager.GenerateSkill(attackSkill);
        }


        /// <summary>
        /// 使用技能攻击
        /// </summary>
        /// <param name="skillID">技能编号</param>
        /// <param name="isBatter">是否连击</param>
        public void AttackUseSkill(int skillID, bool isBatter)
        {
            //如果连击，获取下一个技能编号。
            if (attackSkill != null && isBatter)
                skillID = attackSkill.nextBatterId;

            //准备技能
            attackSkill = skillManager.PrepareSkill(skillID);
            if (attackSkill == null) return;

            //queue.Enqueue(attackSkill);

            //如果成功则播放动画(生成技能通过动画事件调用)
            anim.SetBool(attackSkill.animationName, true);

            if (attackSkill.attackType != SkillAttackType.Single) return;
            //如果单攻，朝向目标(根据当前技能信息查找目标)
            var targetTF = SelectTarget();
            transform.LookAt(targetTF);
            //选中目标，选中其他目标时，先取消上一个选中目标。 
            ShowSelectedFx(false);
            attackTargetTF = targetTF;
            ShowSelectedFx(true);
            //CharacterSelected  角色选择器
            //附加在敌人物体中
            //提供选中/取消功能(等待指定时间自动取消选中) 
        }

        /// <summary>
        /// 获取敌人
        /// </summary>
        /// <returns></returns>
        private Transform SelectTarget()
        {
            //通过扇形选区 获取目标
            var list = new SectorAttackSelector().SelectTarget(attackSkill, transform);
            return list != null ? list[0] : null;
        }

        /// <summary>
        /// 显示锁定目标
        /// </summary>
        /// <param name="state"></param>
        private void ShowSelectedFx(bool state)
        {
            if (attackTargetTF == null) return;
            //获取选中目标中
            var selected = attackTargetTF.GetComponent<CharacterSelected>();
            if (selected != null) selected.SetSelectedActive(state);
        }

        /// <summary>
        /// 使用随机技能攻击(为NPC提供)
        /// </summary>
        public void UseRandomSkill()
        {
            //1.筛选可以使用的技能
            var usableSkills = skillManager.skills.FindAll(s => skillManager.PrepareSkill(s.skillID) != null);

            //2.随机选择一个
            if (usableSkills.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, usableSkills.Count);
                int skillId = usableSkills[index].skillID;
                AttackUseSkill(skillId, false);
            }
        }
    }
}