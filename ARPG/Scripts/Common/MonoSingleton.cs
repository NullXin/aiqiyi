using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	///<summary>
	/// 脚本单例类
	///</summary>
	public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T>
	{
        private static T instance;
        //按需加载
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    //在场景中查找该类型实例
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    { 
                        //创建该类型组件对象
                        instance = new GameObject("Singleto of "+typeof(T).ToString()).AddComponent<T>();
                    }
                    instance.Init();
                }
                return instance;
            }
        }

        private void Awake()
        {
            //如果当前组件在场景中默认存在  则通过as为字段赋值
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
        } 

        public virtual void Init(){ }
    }
}