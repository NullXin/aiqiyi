using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wzx {
	/// <summary>
	/// 
	/// xiaoxin
	/// </summary>
	public abstract class BaseUI : MonoBehaviour {

		[HideInInspector]
		public float CountTime; //倒计时时间
		protected float StartTime; //游戏开始时间
		protected float RemainTime; //剩余时间
		protected bool IsStart=false ;//是否可以倒计时
		//添加钩子
		private void OnEnable() {
			Timer();
            GameManager.Instance.UpdateUIHandle+=UpdateUI;
        }
		
		//移出钩子
		private void OnDisable() {
            GameManager.Instance.UpdateUIHandle-=UpdateUI;
        }
		
		//更新UI
		protected virtual void UpdateUI(){}

		/// <summary>
		/// 查找子物体
		/// </summary>
		/// <param name="tf"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		protected Transform FindChildByName(Transform tf,string name){
			Transform trans=tf.Find(name);
			if(trans!=null) return trans;

			for(int i=0;i<tf.childCount;i++){
				trans= FindChildByName(tf.GetChild(i),name);
				if(trans!=null ) return trans;
			}
			
			return null;
		}

		/// <summary>
		/// 开启倒计时
		/// </summary>
		public void Timer(){
			StartCoroutine(DelayTime()); 
		}

		/// <summary>
		/// 倒计时
		/// </summary>
		/// <returns></returns>
		IEnumerator DelayTime(){
			Text  delayTimeText=FindChildByName(transform,"DelayTime").GetComponent<Text>();
			for(int i=UIManager.Instance.DelayTimer; i>=0 ;i--){
				delayTimeText.text=i+"";
				yield return new WaitForSeconds(1);
			}
			IsStart=true;
			delayTimeText.gameObject.SetActive(false);
			StartTime = Time.time;
			UIManager.Instance.CreateFriut(true);
		}
		
	}
}
