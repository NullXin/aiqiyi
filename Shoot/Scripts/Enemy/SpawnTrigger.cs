using UnityEngine;

namespace wzx
{
    public class SpawnTrigger : MonoBehaviour
    {
        public GameObject spawnGO;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.MAINCAMREA || other.tag=="Player")
            {
                //启用敌人生成器
                spawnGO.SetActive(true);
                //禁用当前物体
                gameObject.SetActive(false); 
            }
        }
    }
}