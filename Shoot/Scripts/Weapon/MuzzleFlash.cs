using UnityEngine;
using System.Collections;

namespace wzx
{ 
    public class MuzzleFlash : MonoBehaviour
    {
        /// <summary>
        /// 枪口火光游戏对象
        /// </summary>
        public GameObject flashGo;

        /// <summary>
        /// 火花显示时间
        /// </summary>
        public float displayTime = 0.3f;

        //隐藏计时器
        private float hideTimer;

        private void Update()
        {
            //如果火花启用，并且到了禁用时间，则
            if (hideTimer <= Time.time)
                HideFlash();
        }

        /// <summary>
        /// 显示火花
        /// </summary>
        public void DisplayFlash()
        {
            enabled = true;
            flashGo.SetActive(true);
            hideTimer = Time.time + displayTime;
        }

        private void HideFlash()
        {
            enabled = false;
            flashGo.SetActive(false);
        }
    }
}