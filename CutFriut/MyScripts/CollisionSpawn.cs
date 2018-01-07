using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace wzx {

	/// <summary>
	/// 武器碰撞类
	/// xiaoxin
	/// </summary>
	public class CollisionSpawn : MonoBehaviour {
		public Material capMaterial;

		private void OnTriggerEnter(Collider other)
		{
			GameObject victim = other.gameObject;
			if (other.gameObject.tag == "Fruit") {
				CutFruit (victim);
				StartCoroutine(DelayLoadScene(1,1f));
			}

			if (other.gameObject.tag == "Fruit2") {
				CutFruit (victim);
				StartCoroutine(DelayLoadScene(2,1f));
			}

			if (victim.tag == "Melon") {
				GameManager.Instance.AddScore(10);
				CutFruit (victim);
			}
			if (victim.tag == "Apple") {
				GameManager.Instance.AddScore(10);
				CutFruit (victim);
			}
			if (victim.tag == "Pear") {
				GameManager.Instance.AddScore(10);
				CutFruit (victim);
			}
			if (victim.tag == "Orange") {
				GameManager.Instance.AddScore(10);
				CutFruit (victim);
			}
			if (victim.tag == "Banana") {
				GameManager.Instance.AddScore(10);
				CutFruit (victim);
			}
		}

		private IEnumerator	DelayLoadScene(int index, float delay){
			yield return new WaitForSeconds(delay);
			SceneManager.LoadScene(index);
		}

		/// <summary>
		///  切掉水果
		/// </summary>
		/// <param name="victim"></param>
		private void CutFruit (GameObject victim) {
			SoundManager._Instance.PlaySplatter (); //添加声音
			GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.right, capMaterial);
			//在MeshCut223行加一行代码  切得物体不一般大
			//Instantiate(melonPar, other.gameObject.transform.position, Quaternion.identity);
			if (!pieces[0].GetComponent<Rigidbody> ())
				pieces[0].AddComponent<Rigidbody> ();
			if (!pieces[1].GetComponent<Rigidbody> ())
				pieces[1].AddComponent<Rigidbody> ();
			//1表示切掉的部分  0 表示保留的部分
			Destroy (pieces[1], 1);
			//勾掉地形的碰撞  吧刀上的物理属性勾上
			//会发现被切物体大小与切掉的物体 不一般大 修改MeshCut的220代码
			// 加一行 rightSideObj.transform.localScale = victim.transform.localScale;//让被切掉的物体和原物体一般大
			victim.GetComponent<MeshCollider>().enabled=false;
		}
	}
}