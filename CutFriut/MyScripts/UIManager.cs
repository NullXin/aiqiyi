using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Common;
using System;

namespace wzx {



	/// <summary>
	/// 
	/// xiaoxin
	/// </summary>
	public class UIManager : MonoSingleton<UIManager>{
		[Tooltip("倒计时")]
		public int[] second;
		[Tooltip("水果生成位置")]
		public GameObject[] fruitPositions;
		[Tooltip("界面管理")]
		public List<BaseUI> listUI;
		[Tooltip("特效")]
		public GameObject[] effets;

		//下标
		public int index{get;private set;}
		[Tooltip("倒计时")]
		public int DelayTimer=3;

		protected override void Init(){
			index=0;
			listUI=new List<BaseUI>();

			BaseUI[] uis = FindObjectsOfType<BaseUI>();
			//Array.Reverse(uis);
			
			foreach (var item in uis)
			{
				item.gameObject.SetActive(false);
				listUI.Add(item);
			}

			foreach (var item in fruitPositions)
			{
				item.SetActive(false);
			}
			listUI[index].CountTime = second[index];
			listUI[index].gameObject.SetActive(true);
		}

		/// <summary>
		/// 切换UI
		/// </summary>
		public void ChangeUI(){

			if(index==listUI.Count-1){ 
				//游戏结束
				listUI[index].gameObject.SetActive(false);
				fruitPositions[index].SetActive(false);
				GameManager.Instance.GameOver();
				return;
			}

			listUI[index].gameObject.SetActive(false);
			effets[index].SetActive(true);
			CreateFriut (false);
		}

		public void CreateFriut(bool state){
			fruitPositions[index].SetActive(state);
		}
		
		public void TriggerShowUI(){
			index++;
			listUI[index].gameObject.SetActive(true);
			listUI[index].CountTime=second[index];
		}



		// public GameObject[] FruitPrefabs;
		// public Transform FruitStartPosition;
		// public float WaitTime = 3; //没波等待时间

		// public float StartTime; //游戏开始时间
		// public float CountTime = 40; //倒计时时间
		// private float RemainTime; //剩余时间

		// public Text ScoreText; //分数显示
		// public Text TimeText; //时间显示

		// public float TotalScore; //总分

		
		// void Awake () {
		// 	StartRandomFruit ();
		// }

		// /// <summary>
		// /// 开启随机生成水果
		// /// </summary>
		// public void StartRandomFruit () {
		// 	StartCoroutine(SpawnFruit ());
		// }

		// /// <summary>
		// /// 建立水果
		// /// </summary>
		// /// <returns></returns>
		// private IEnumerator SpawnFruit () {
		// 	while (true) {
		// 		int num = Random.Range (0, 5);
		// 		for (int i = 0; i < num; i++) {
		// 			SpawnFruitOne ();
		// 			yield return new WaitForSeconds (Random.Range (0.1f, 0.99f));
		// 		}
		// 		yield return new WaitForSeconds (WaitTime);
		// 	}
		// }

		// /// <summary>
		// /// 建立水果
		// /// </summary>
		// private void SpawnFruitOne () {
		// 	GameObject Friut = Instantiate (FruitPrefabs[Random.Range (0, FruitPrefabs.Length)]);
		// 	Friut.transform.position = FruitStartPosition.position + Vector3.right * Random.Range (-1.5f, 1.5f);
		// 	Rigidbody rigidbody = Friut.GetComponent<Rigidbody> ();
		// 	rigidbody.velocity = new Vector3 (0, 5, 0);
		// 	rigidbody.angularVelocity = new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5));
		// 	rigidbody.useGravity = true;
		// }

		// void FixedUpdate () {
		// 	//倒计时实现
		// 	RemainTime=CountTime-(Time.time-StartTime);
			
		// 	if(RemainTime <= 0){
		// 		GameOver();
		// 		TimeText.text="时间:"+FormatTime(0);
		// 		return;
		// 	}

		// 	TimeText.text="时间:"+FormatTime(RemainTime);
		// }

        // private void GameOver()
        // {
            
        // }

        // /// <summary>
        // /// 时间格式化
        // /// </summary>
        // /// <param name="timeRemain"></param>
        // /// <returns></returns>

        // private string FormatTime(float timef)
        // {
        //     int timeFormat=(int)timef;
		// 	return timeFormat>=10 ? "00:"+timeFormat: "00:0"+timeFormat;
        // }

		// /// <summary>
		// /// 添加分数
		// /// </summary>
		// /// <param name="score"></param>
		// public void AddScore(float score){
		// 	TotalScore +=score;
		// 	ScoreText.text="分数:"+TotalScore+"";
		// }


		

    }
}