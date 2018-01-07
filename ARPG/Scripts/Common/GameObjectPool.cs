using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    public interface IResetable
    {
        void OnReset();
    }

    ///<summary>
    ///
    ///</summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        private Dictionary<string, List<GameObject>> cache;

        private void Awake()
        {
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 使用对象池创建对象
        /// </summary>
        /// <param name="key">对象种类</param>
        /// <param name="prefab">对象预制件</param>
        /// <param name="pos">位置</param>
        /// <param name="dir">旋转</param>
        /// <returns></returns>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion dir)
        {
            //1. 在池中查找可以使用的对象
            GameObject go = FindUsableObject(key);
            //2. 如果没有找到，则创建再加入池中。
            if (go == null)
            {
                go = Instantiate(prefab);
                Add(key, go);
            }
            //3.使用对象
            UseObject(pos, dir, go);
            return go;
        }

        private void UseObject(Vector3 pos, Quaternion dir, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = dir;
            go.SetActive(true);
            //遍历物体中所有需要重置的脚本
            foreach (var item in go.GetComponents<IResetable>())
            {
                item.OnReset();
            }
        }

        private void Add(string key, GameObject go)
        {
            if (!cache.ContainsKey(key)) cache.Add(key, new List<GameObject>());
            cache[key].Add(go);
        }

        private GameObject FindUsableObject(string key)
        {
            //字典如果存在当前记录，则在集合中查找禁用的物体
            if (cache.ContainsKey(key))
                return cache[key].Find(o => !o.activeInHierarchy);
            return null;
        }

        /// <summary>
        /// 立即回收
        /// </summary>
        /// <param name="go"></param>
        public void CollectObject(GameObject go)
        {
            go.SetActive(false);
        }

        /// <summary>
        /// 延迟回收
        /// </summary>
        /// <param name="go"></param>
        public void CollectObject(GameObject go,float delay)
        {
            StartCoroutine(DelayCollect(go, delay));
        }

        private IEnumerator DelayCollect(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            CollectObject(go);
        }

        public void Clear(string key)
        {
            //释放游戏对象
            foreach (var item in cache[key])
            {
                Destroy(item);
            }
            //移除键
            cache.Remove(key);
        }
         
        public void ClearAll()
        {
            List<string> keyList = new List<string>(cache.Keys);
            foreach (var item in keyList)
            {
                //遍历集合  移除字典记录
                Clear(item);
            } 
        }
    }
}