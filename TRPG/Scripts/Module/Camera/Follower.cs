using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRPG.Module
{
	///<summary>
	/// 相机跟随玩家
	///</summary>
	public class Follower:MonoBehaviour
	{
        private Transform target; // 跟随目标(Player)
        private Vector3 offset; // 目标点和相机直接的距离
        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag( "Player" ).transform;
            offset = target.position - transform.position; // 获取距离
        }

        private Vector3 tmp;
        private Vector3 speed;
        void Update()
        {
            tmp = target.position - offset; // 保持距离不变
            // 边界处理
            // z 0-35
            if (tmp.z < 0) tmp.z = 0;
            // x -35  - 0


            ///transform.position = tmp;
            // 缓动效果
            transform.position = Vector3.SmoothDamp( transform.position, tmp, ref speed, 0.2f );
        }
    }
}