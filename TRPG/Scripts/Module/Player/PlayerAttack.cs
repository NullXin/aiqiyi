using UnityEngine;
using System.Collections;

namespace TRPG.Module
{
    /// <summary>
    /// 玩家技能释放
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        private SkillManager sm; // SkillManager 用来释放技能
        public SkillInfo normalAttack; // 普攻
        public SkillInfo s1; // 大招1
        public SkillInfo s2; // 大招2
        void Awake()
        {
            // 找到所有的ETCButton
            ETCButton[] btns = GameObject.FindObjectsOfType<ETCButton>();
            // 统一添加OnDown事件处理函数
            foreach (ETCButton btn in btns)
                btn.onDown.AddListener( OnDown );
            // 获取SkillManager
            sm = GetComponent<SkillManager>();
        }

        private void Start()
        {
            // 从PlayerPrefs中读取设置的技能
            if (GetSkill( PlayerPrefs.GetString( "s0" ) ) != null)
                s1 = GetSkill( PlayerPrefs.GetString( "s0" ) );
            if (GetSkill( PlayerPrefs.GetString( "s1" ) ) != null)
                s2 = GetSkill( PlayerPrefs.GetString( "s1" ) );

        }
        public SkillInfo[] skills; // 所有技能
        /// <summary>
        /// 根据技能名称找到技能实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SkillInfo GetSkill(string name)
        {
            SkillInfo skill = null;
            foreach( SkillInfo s in skills)
            {
                if (s.name == name) return s;
            }
            return skill;
        }
        /// <summary>
        /// OnDown事件处理函数
        /// </summary>
        /// <param name="s"></param>
        private void OnDown( string s) // 
        {
            print( s );
            switch (s) // 使用按钮名称区分按下的是哪个按钮
            {
                case "normalAttack": // 普攻
                    sm.Fire( normalAttack );
                    break;
                case "attack1": // 大招1
                    sm.Fire( s1 );
                    break;
                case "attack2": // 大招2
                    sm.Fire( s2 );
                    break;
                default:
                    Debug.LogError( "未处理的attack " + s );
                    break;
            }
        }
        
    }
}

