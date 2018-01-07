
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ARPGDemo.Skill
{
	/// <summary>
	/// 释放器配置工厂
	/// </summary>
	public class DeployerConfigFactory
	{
        //对象池
        private static Dictionary<string, object> cache;

        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, object>();
        }

        /// <summary>
        /// 创建攻击区域类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IAttackSelector CreateAttackSelector(SkillData data)
        {
            string classNameAll = "ARPGDemo.Skill." + data.selectorType + "AttackSelector";
            return CreateObject<IAttackSelector>(classNameAll);
        }

        /// <summary>
        /// 创建受影响对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<IImpact> CreateAttackImpact(SkillData data)
        {
            List<IImpact> list = new List<IImpact>(data.impactType.Length);
            for(int i = 0; i < data.impactType.Length; i++)
            {
                string classNameAll = "ARPGDemo.Skill." + data.impactType[i]+ "Impact";
                list.Add(CreateObject<IImpact>(classNameAll));
            }
            return list;
        }

        /// <summary>
        /// 反射创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classNameAll"></param>
        /// <returns></returns>
        private static T CreateObject<T>(string classNameAll) where T : class
        {
            //先判断池中是否具有可以使用的对象
            if (!cache.ContainsKey(classNameAll))
            {
                //如果没有则创建对象并添加到池中
                Type type = Type.GetType(classNameAll);
                cache.Add(classNameAll, Activator.CreateInstance(type));
            }
            //从池中返回对象
            return cache[classNameAll] as T;
        }
    }
}