using UnityEngine;
using System.Collections;

namespace wzx
{
    /// <summary>
    /// 子弹，负责根据方向检测目标点进行移动，到达目标点后根据标签创建特效
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        protected Vector3 targetPoint;

        protected float atk;
        public virtual void Init(float atk)
        {
            this.atk = atk;
            //计算目标点
            CalculateTargetPoint();
        }

        /// <summary>
        /// 攻击距离
        /// </summary>
        public float attackDistance = 200;
        /// <summary>
        /// 攻击目标射线检测层
        /// </summary>
        public LayerMask targetMask;

        //敌人子弹：检测环境  玩家
        //玩家子弹：检测敌人与环境
        protected RaycastHit hit;
        private void CalculateTargetPoint()
        {
            //射线检测：从枪口位置，沿子弹正前方，检测敌人与环境
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackDistance, targetMask))
                targetPoint = hit.point;
            else
                targetPoint = transform.TransformPoint(0, 0, attackDistance);
        }

        private void Update()
        {
            Movement();

            //如果接触目标点  
            //if ((transform.position - targetPoint).sqrMagnitude < 0.1f)
            if(Vector3.Distance(transform.position,targetPoint)<0.1f)
            {
                //到达目标
                ArriveTargetPoint(); 
            }
        }

        public float moveSpeed = 100;
        private void Movement()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * moveSpeed);
        }
        
        public float force = 10;
        public float radius = 1;

        //生成接触效果
        private void GenerateContactEffect()
        { 
            if (hit.collider == null) return;
             
            //特效预制件名：Effects +  标签
            string prefabName = "Effects" + hit.collider.tag;
            
            var contactEffectPrefab = ResourceManager.Load<GameObject>(prefabName);

            if (contactEffectPrefab)
            {
                GameObject go = Instantiate(contactEffectPrefab, targetPoint + hit.normal * 0.005f, Quaternion.LookRotation(hit.normal));
                go.transform.SetParent(hit.transform);
            }
        }

        //到达目标点
        protected virtual void ArriveTargetPoint()
        { 
            Destroy(gameObject);

            GenerateContactEffect();
            Rigidbody rdb=null;
             if (hit.collider)rdb = hit.collider.GetComponent<Rigidbody>();
            if (rdb && !rdb.isKinematic) rdb.AddForce(-hit.normal * force);
        }
    }
}