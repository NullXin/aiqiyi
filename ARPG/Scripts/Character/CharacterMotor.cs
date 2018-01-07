using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色马达：负责角色运动。
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        [Tooltip("旋转速度")]
        public float rotateSpeed = 50;
        [Tooltip("移动速度")]
        public float moveSpeed = 4;

        private CharacterController chController;

        private void Start()
        {
            int[] arr = new int[3];
            int[] arr2 = arr;
            chController = GetComponent<CharacterController>();
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="targetDir">目标方向</param>
        public void Movement(Vector3 targetDir)
        {
            LookDirection(targetDir);
            //通过Unity CharacterController 组件向前移动移动Move
            Vector3 dir = transform.forward;
            dir.y = -1;//模拟重力
            chController.Move(dir * Time.deltaTime * moveSpeed); 
        }

        /// <summary>
        /// 注视方向旋转
        /// </summary>
        /// <param name="targetDir">目标方向</param>
        public void LookDirection(Vector3 targetDir)
        {
            if (targetDir == Vector3.zero) return;
            //注视旋转到目标方向
            Quaternion dir = Quaternion.LookRotation(targetDir);
            //插值旋转（由快到慢）
            transform.rotation = Quaternion.Lerp(transform.rotation, dir, Time.deltaTime * rotateSpeed);
        } 
    }
}