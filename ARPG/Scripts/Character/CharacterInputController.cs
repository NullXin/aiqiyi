using ARPGDemo.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
	///<summary>
	///角色输入控制器：获取用户输入。
	///</summary>
    [RequireComponent(typeof(CharacterMotor))]
	public class CharacterInputController : MonoBehaviour
	{
        [Tooltip("最小攻击间隔")]
        public float minAttackInterval = 1.5f;
        [Tooltip("最大连击时间")]
        public float maxBatterTime = 3;
        //最后按下的时间
        private float lastPressedTime;
        //操纵杆类
        private ETCJoystick joystick;
        //角色马达类
        private CharacterMotor chMotor;
        //所有Button按钮
        private ETCButton[] skillButtons;
        //动画控制器
        private Animator anim;
        //玩家状态类
        private PlayerStatus chStatus;
        //角色技能系统
        private CharactorSkillSystem skillSystem;

        //查找组件
        private void Awake()
        {
            //获取操纵杆
            joystick = FindObjectOfType<ETCJoystick>();
            //获取角色运动
            chMotor = GetComponent<CharacterMotor>();
            //获取所有技能按钮
            skillButtons = FindObjectsOfType<ETCButton>();
            //获取动画控制器
            anim = GetComponentInChildren<Animator>();
            //获取玩家状态
            chStatus = GetComponent<PlayerStatus>();
            //技能系统类
            skillSystem = GetComponent<CharactorSkillSystem>();
        }

        //注册事件
        private void OnEnable()
        {
            joystick.onMove.AddListener(OnJoystickMove);
            joystick.onMoveStart.AddListener(OnJoystickMoveStart);
            joystick.onMoveEnd.AddListener(OnJoystickMoveEnd);

            foreach (var item in skillButtons)
            {
                if (item.name == "BaseSkill")
                    item.onPressed.AddListener(OnButtonPressed);
                else
                    item.onDown.AddListener(OnButtonDown);
            }
        } 
        
        //注销事件
        private void OnDisable()
        {
            joystick.onMove.RemoveListener(OnJoystickMove);
            joystick.onMoveStart.RemoveListener(OnJoystickMoveStart);
            joystick.onMoveEnd.RemoveListener(OnJoystickMoveEnd);

            foreach (var item in skillButtons)
            {
                if (!item) continue; // 如果安安被销毁则跳过
                if (item.name == "BaseSkill")
                    item.onPressed.RemoveListener(OnButtonPressed);
                else
                    item.onDown.RemoveListener(OnButtonDown);
            }
        }

        /// <summary>
        /// 玩家移动事件
        /// </summary>
        /// <param name="dir"></param>
        private void OnJoystickMove(Vector2 dir)
        {
            //摇杆 dir ：  x        y 
            //马达 dir：   x   0   z
            //调用马达移动方法
            if (chStatus.IsAttacking()) { anim.SetBool(chStatus.animParams.Run, false); return; }
            anim.SetBool(chStatus.animParams.Run, true);
            Vector3 targetDir = new Vector3(dir.x, 0, dir.y);
            chMotor.Movement(targetDir);
        }

        /// <summary>
        /// 操纵杆移动开始
        /// </summary>
        private void OnJoystickMoveStart()
        {
            anim.SetBool(chStatus.animParams.Run, true);
        }

        /// <summary>
        /// 操纵杆移动结束
        /// </summary>
        private void OnJoystickMoveEnd()
        {
            anim.SetBool(chStatus.animParams.Run, false);
        }


        /// <summary>
        /// 按钮绑定事件
        /// </summary>
        /// <param name="buttonName"></param>
        private void OnButtonDown(string buttonName)
        {
            if (chStatus.IsAttacking()) return;
            int skillId = 0;
            switch (buttonName)
            {
                case "BaseSkill":
                    skillId = 1001;
                    break;
                case "Skill1":
                    skillId = 1002;
                    break;
                case "Skill2":
                    skillId = 1003;
                    break;
            }
            //在技能系统中使用攻击技能
            skillSystem.AttackUseSkill(skillId,false);
        }

        /// <summary>
        /// 按钮一直按下的时候调用
        /// </summary>
        private void OnButtonPressed()
        {
            if (chStatus.IsAttacking()) return;
            //计算间隔 = 当前时间 - 最后按下时间 
            var interval = Time.unscaledTime - lastPressedTime;
            //如果间隔过小  则退出
            if (interval < minAttackInterval) return;
            //间隔 <  最大连击时间  可以连击  否则不可以连击
            bool isBatter = interval < maxBatterTime;
            //攻击
            skillSystem.AttackUseSkill(1001, isBatter);
            //记录最后按下时间 
            lastPressedTime = Time.unscaledTime;
        }
    }
}