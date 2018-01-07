using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wzx {

	/// <summary>
	/// 
	/// xiaoxin
	/// </summary>
	public class PlayerTrigger : MonoBehaviour {
		public Transform pointer;

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				transform.parent.gameObject.SetActive(false);
				//传送玩家
				UIManager.Instance.TriggerShowUI();
				other.transform.position = pointer.position;
			}
		}
	}
}
