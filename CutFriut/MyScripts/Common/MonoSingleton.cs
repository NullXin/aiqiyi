using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common {

	/// <summary>
	/// 
	/// xiaoxin
	/// </summary>
	public class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T> {
		private static T instance;
		public static T Instance{
			get{
				if(instance==null){
					instance=FindObjectOfType<T>();
					if(instance==null){
						GameObject go=new GameObject("MonoSingleton of"+typeof(T).ToString());
						go.AddComponent<T>();
					}
				}
				return instance;
			}
		}

        private void Awake()
        {
			if(instance==null){
            	instance = FindObjectOfType<T>();
			}
			Init();
        }

		protected virtual void Init(){}
    }
}
