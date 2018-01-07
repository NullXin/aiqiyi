using UnityEngine;
using System.Collections; 
using AI.FSM;
using Common; 

namespace wzx
{
    /// <summary>
    /// 敌人生成器
    /// </summary>
    public class EnemySpawn : MonoBehaviour 
    { 
        [Tooltip("敌人类型预制件")]
        public GameObject[] enemyTypes;

        [Tooltip("产生敌人最大延迟时间")]
        public float maxDelay = 10;

        //数组：一维数组    多维数组  交错
        //         []              [,]          [][]
        private Vector3[][] wayPaths;//每行(路线)的列数(路点)不同

        private int diedCount;

        public void Start()
        { 
            CalculateWayLines();

            if (wayPaths.Length == 0)
            {
                CompleteTask();
                return;
            }

            StartCoroutine(GenerateEnemy());
        }
          
        //计算路线 交错数组
        //      坐标 交错数组元素的元素
        private void CalculateWayLines()
        {
            //根据子物体(路线)数量 创建交错数组
            wayPaths = new Vector3[transform.childCount][];
            for (int pathIndex = 0; pathIndex < wayPaths.Length; pathIndex++)
            {
                //路线变换组件
                Transform wayPathTF = transform.GetChild(pathIndex);
                //根据孙子物体(路点)数量 创建交错数组元素(一维数组)
                wayPaths[pathIndex] = new Vector3[wayPathTF.childCount];
                for (int pointIndex = 0; pointIndex < wayPathTF.childCount; pointIndex++)
                {
                    // 将路点坐标存入交错数组
                    wayPaths[pathIndex][pointIndex] = wayPathTF.GetChild(pointIndex).position;
                }
            }
        }
         
        //生成敌人
        private IEnumerator GenerateEnemy()
        {
            //交错数组.Length  --->  行数
            for (int i = 0; i < wayPaths.Length; i++)
            {
                float delay = Random.Range(1, maxDelay);
                yield return new WaitForSeconds(delay);
                var prefab = enemyTypes[Random.Range(0, enemyTypes.Length)]; 
                CreateEnemy(prefab, wayPaths[i]);
            } 
        } 
         
        private void CreateEnemy(GameObject prefab, Vector3[] path)
        {    
            //创建敌人游戏对象
            GameObject enemy = Instantiate(prefab, path[0], Quaternion.identity);

            //配置信息 
            enemy.GetComponent<BaseFSM>().wayPath = path;

            //注册敌人死亡事件
            enemy.GetComponent<EnemyStatus>().OnDeathHandler += IsComplete;
        }

        private void IsComplete()
        {
            if (++diedCount == wayPaths.Length)
                CompleteTask();
        }
         
        private void CompleteTask()
        {
            //激活下一个生成器
            SpawnSystem.Instance.ActivateNextSpawn();  
        }
    }
}