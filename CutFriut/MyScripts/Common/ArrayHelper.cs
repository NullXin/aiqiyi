using System;
using System.Collections.Generic; 

/*
c# 扩展方法
    定义：能够向现有类添加方法，而无需修改原始类或创建新的派生类。
    要素：
    1.扩展方法必须在非泛型的静态类中。
    2.第一个参数使用this关键字指定被扩展类型。
    3.建议在扩展方法所在类上，添加命名空间。
*/

namespace Common
{
    ///<summary>
    /// 数组助手
    ///</summary>
    public static class ArrayHelper 
    {
        /// <summary>
        /// 对象数组的升序排列
        /// </summary>
        /// <typeparam name="T">对象的类型 例如：Enemy</typeparam>
        /// <typeparam name="Q">对象的属性 例如：HP</typeparam>
        /// <param name="array">对象数组</param>
        /// <param name="handler">排序依据</param>
        public static void OrderBy<T,Q>(this T[] array,Func<T,Q> handler) where Q : IComparable  
        { 
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i+1; j < array.Length; j++)
                {
                    //if (array[i].ATK > array[j].ATK)
                    //if(Fun1(array[i]) > Fun1(array[j]) )
                    //if(handler(array[i])>handler(array[j]))
                    if (handler(array[i]).CompareTo(handler(array[j])) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 对象数组的降序排列
        /// </summary>
        /// <typeparam name="T">对象的类型 例如：Enemy</typeparam>
        /// <typeparam name="Q">对象的属性 例如：HP</typeparam>
        /// <param name="array">对象数组</param>
        /// <param name="handler">排序依据</param>
        public static void OrderByDescending<T, Q>(this T[] array, Func<T, Q> handler) where Q : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                { 
                    if (handler(array[i]).CompareTo(handler(array[j])) < 0)
                    {
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        //练习2：
        //查找满足条件的所有对象
        //应用：查找所有活的敌人
        //public static EnemyTest[] FindAll(EnemyTest[] array)
        //{
        //    List<EnemyTest> list = new List<EnemyTest>(array.Length);
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        if (array[i].HP > 0)
        //        {
        //            list.Add(array[i]);
        //        }
        //    }
        //    return list.ToArray();
        //}

        /// <summary>
        /// 查找所有满足条件的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="array">对象数组</param>
        /// <param name="condition">查找条件</param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] array,Func<T,bool> condition)
        {
            List<T> list = new List<T>(array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    list.Add(array[i]);
                }
            }
            return list.ToArray();
        }
         
        //练习3：筛选对象
        //应用：GameObject[]   -->  Transform[]
        //public static Transform[] Select<Q>(GameObject[] array)
        //{
        //    Transform[] newArray = new Transform[array.Length];
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        newArray[i] = array[i].transform;
        //    }
        //    return newArray;
        //}
        /// <summary>
        /// 筛选对象数组
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="Q">筛选结果类型</typeparam>
        /// <param name="array">需要筛选的对象数组</param>
        /// <param name="handler">筛选逻辑</param>
        /// <returns></returns>
        public static Q[] Select<T,Q>(this T[] array, Func<T, Q> handler)
        {
            Q[] newArray = new Q[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = handler(array[i]);
            }
            return newArray;
        }

        //练习4：查找指定条件的最大元素
        //应用：获取血量最高的敌人
        //               最近的敌人
        //5  7    2   9    1    3
        //public static int GetMax(int[] array)
        //{
        //    int max = array[0];
        //    for (int i = 1; i < array.Length; i++)
        //    {
        //        if (max < array[i])
        //        {
        //            max = array[i];
        //        }
        //    }
        //    return max;
        //}

        /// <summary>
        /// 获取指定条件的最大元素
        /// </summary>
        /// <typeparam name="T">需要查找的对象类型</typeparam>
        /// <typeparam name="Q">需要查找的对象属性</typeparam>
        /// <param name="array">需要查找的对象数组</param>
        /// <param name="condition">查找条件</param>
        /// <returns></returns>
        public static T GetMax<T,Q>(this T[] array,Func<T,Q> condition) where Q : IComparable
        {
            T max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                //if (max < array[i])
                if(condition(max).CompareTo(condition(array[i])) < 0 )
                {
                    max = array[i];
                }
            }
            return max;
        }
    }
}