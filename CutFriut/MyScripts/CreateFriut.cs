using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wzx {

	/// <summary>
	/// 水果生成
	/// xiaoxin
	/// </summary>
	public class CreateFriut : MonoBehaviour {
		[Tooltip("水果的预制体")]
		public GameObject[] FruitPrefabs;
		[Tooltip("每波生成的时间")]
		public float WaitTime = 3;

		/// <summary>
		/// 开启随机生成水果
		/// </summary>
		public void StartRandomFruit () {
			StartCoroutine (SpawnFruit ());
		}

		public void StopRandomFruit(){
			StopCoroutine (SpawnFruit ());
		}

		/// <summary>
		/// 建立水果
		/// </summary>
		/// <returns></returns>
		private IEnumerator SpawnFruit () {
			while (true) {
				int num = Random.Range (0,8);
				for (int i = 0; i < num; i++) {
					SpawnFruitOne ();
					yield return new WaitForSeconds (Random.Range (0.1f, 0.5f));
				}
				yield return new WaitForSeconds (WaitTime);
			}
		}

		/// <summary>
		/// 建立一个水果
		/// </summary>
		private void SpawnFruitOne () {
			GameObject Friut = Instantiate (FruitPrefabs[Random.Range (0, FruitPrefabs.Length)]);
			Friut.transform.position = transform.position + Vector3.right * Random.Range (-1f, 1f);
			Rigidbody rigidbody = Friut.GetComponent<Rigidbody> ();
			rigidbody.velocity = new Vector3 (0, 5, 0);
			rigidbody.angularVelocity = new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5));
			rigidbody.useGravity = true;
		}

		// void Awake () {
		// 	StartRandomFruit ();
		// }
	}
}
