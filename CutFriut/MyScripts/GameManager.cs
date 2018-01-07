using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace wzx {

	/// <summary>
	/// 
	/// xiaoxin
	/// </summary>
	public class GameManager :MonoSingleton<GameManager>{
		public event Action UpdateUIHandle;

		//[Tooltip("总分")]
		[HideInInspector]
		public float TotalScore; //总分

        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="timeRemain"></param>
        /// <returns></returns>
        public string FormatTime(float timef)
        {
            int timeFormat=(int)timef;
			return timeFormat>=10 ? "00:"+timeFormat: "00:0"+timeFormat;
        }
		
		/// <summary>
		/// 添加分数
		/// </summary>
		/// <param name="score"></param>
		public void AddScore(float score){
			TotalScore +=score;

			if(UpdateUIHandle!=null){
				UpdateUIHandle();
			}
		}

		public void GameOver(){
			print("游戏结束");
			//	Cursor.lockState = CursorLockMode.None;
			UI_Keyboard.Instance.gameObject.SetActive(true);
		}
	}
}
