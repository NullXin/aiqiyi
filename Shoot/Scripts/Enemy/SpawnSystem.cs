using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

namespace wzx
{
    /// <summary>
    /// 生成器系统
    /// </summary>
    public class SpawnSystem : MonoSingleton<SpawnSystem>
    {
        private GameObject[] spawnList;

        protected override void Initialized()
        {
            base.Initialized();

            spawnList = new GameObject[transform.childCount];
            for (int i = 0; i < spawnList.Length; i++)
            {
                //获取生成器物体
                spawnList[i] = transform.GetChild(i).gameObject;
                //禁用生成器 Spawn
                spawnList[i].SetActive(false);
                //禁用根路线
                spawnList[i].GetComponentInChildren<EnemySpawn>().gameObject.SetActive(false);
            }
        }

        public int currentIndex = -1;

        //激活下一个生成点
        public void ActivateNextSpawn()
        {
            if (currentIndex != -1)
                spawnList[currentIndex].SetActive(false);

            if (currentIndex < spawnList.Length - 1)
            {
                spawnList[++currentIndex].SetActive(true);
            }
            else
            {
                //游戏结束
                //GameController.Instance.GameOver();
            }
        }
    }
}