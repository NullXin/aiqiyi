using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    ///<summary>
    /// 变换组件助手类
    ///</summary>
    public static class TransformHelper 
    {
        /// <summary>
        /// 未知层级，查找后代物体。
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="childName">需要查找的后代物体名称</param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform tf, string childName)
        {
            //递归：方法内部又调用自身的过程。
            //思想：将问题转移给范围缩小的子问题。
            //步骤：查找指定名称的子物体，如果没有找到，将问题推给子物体。
            //经典面试题：5!  --> 5 * 4 * 3 * 2 * 1
            Transform childTF = tf.Find(childName);
            //如果找到 则退出
            if (childTF != null) return childTF;
            //如果没有找到，则将问题推给子物体。
            for (int i = 0; i < tf.childCount; i++)
            {
                // tf.GetChild(i) 子物体
                childTF = tf.GetChild(i).FindChildByName(childName);
                if (childTF != null) return childTF;
            }
            return null;
        }

        /// <summary>
        /// 缓动注视目标方向旋转
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="targetDir">目标方向</param>
        /// <param name="rotateSpeed">旋转速度</param>
        public static void LookDirection(this Transform tf, Vector3 targetDir, float rotateSpeed)
        {
            if (targetDir == Vector3.zero) return;
            //注视旋转到目标方向
            Quaternion dir = Quaternion.LookRotation(targetDir);
            //插值旋转（由快到慢）
            tf.rotation = Quaternion.Lerp(tf.rotation, dir, Time.deltaTime * rotateSpeed);
        }
     
        /// <summary>
        /// 缓动注视目标点旋转
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="pos">目标位置</param>
        /// <param name="rotateSpeed">旋转速度</param>
        public static void LookPosition(this Transform tf, Vector3 pos, float rotateSpeed)
        {
            Vector3 dir = pos - tf.position;
            tf.LookDirection(dir, rotateSpeed);
        }

        /// <summary>
        /// 计算周边物体
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="distance">距离</param>
        /// <param name="angle">角度</param>
        /// <param name="tags">标签</param>
        /// <returns></returns>
        public static Transform[] CalculateAroundObject(this Transform currentTF, float distance, float angle ,params string[] tags)
        {
            List<Transform> list = new List<Transform>();
            //根据所有标签查找物体
            for (int i = 0; i < tags.Length; i++)
            {
                GameObject[] allGo = GameObject.FindGameObjectsWithTag(tags[i]);
                list.AddRange(allGo.Select(o => o.transform));
            }
            //判断物体是否在攻击范围(距离、角度)
            list = list.FindAll(t => 
                 Vector3.Distance(t.position, currentTF.position) <= distance && 
                 Vector3.Angle(currentTF.forward, t.position - currentTF.position) <= angle / 2
            );
            return list.ToArray();
        }
    }
}