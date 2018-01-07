using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Character;
using Common;


namespace ARPGDemo.Skill
{
    ///<summary>
    /// 技能管理器：主要负责技能释放前的工作。
    ///</summary>
    public class CharacterSkillManager : MonoBehaviour
    {
        //技能列表
        public List<SkillData> skills;

        //技能初始化
        private void Start()
        {   
            //遍历所有技能并初始化
            for (int i = 0; i < skills.Count; i++)
            {
                SkillInit(skills[i]);
            }
        }

        /// <summary>
        ///技能初始化：加载所有预制体和技能所属
        /// </summary>
        /// <param name="data"></param>
        private void SkillInit(SkillData data)
        {
            //加载技能预制体
            data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);
            //加载受伤预制体
            data.hitFxPrefab = ResourceManager.Load<GameObject>(data.hitFxName);
            //技能所属
            data.owner = gameObject;
        }

        /// <summary>
        /// 准备技能:看技能是否符合要求(冷却时间、法力)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SkillData PrepareSkill(int id)
        {
            //根据ID查找技能
            //判断条件
            var data = skills.Find(s => s.skillID == id);
            if (data != null && 
                data.skillPrefab && 
                data.hitFxPrefab &&
                data.coolRemain == 0 && 
                data.costSP <= GetComponent<CharacterStatus>().SP)
                return data;
            return null;
        }

        /// <summary>
        /// 生成技能:对象池中创建技能对象，添加技能释放器，释放技能，开启技能冷却
        /// </summary>
        /// <param name="data"></param>
        public void GenerateSkill(SkillData data)
        {
            if (data == null) return;
            //通过对象池创建技能预制件
            var skillGo = GameObjectPool.Instance.CreateObject(data.prefabName, data.skillPrefab, transform.position, transform.rotation);

            //获取释放器
            var deployer=skillGo.GetComponent<SkillDeployer>();
            if (deployer==null)
            {
               deployer=skillGo.AddComponent<MeleeSkillDeployer>();
            }
            //给释放器添加技能数据
            deployer.CurrentSkillData=data;
            //释放技能
            deployer.DeployerSkill();
            //开启冷却
            StartCoroutine(CoolTimeDown(data));
        }

        /// <summary>
        /// 冷却技能
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerator CoolTimeDown(SkillData data)
        {
            data.coolRemain = data.coolTime;
            //coolRemain 一秒自减一次
            while (data.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
        }
    }
}