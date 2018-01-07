using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace ARPGDemo.Character
{
	/// <summary>
	/// 目标选中类
	/// </summary>
	public class CharacterSelected : MonoBehaviour
	{
        public string selectedName = "selected";
        private GameObject selectGo;
        private float hideTimer;

        private void Awake()
        {
            var tf= transform.FindChildByName(selectedName);
            if (tf != null)
            {
                selectGo = tf.gameObject;
            }
        }


        /// <summary>
        /// 设置选择器的显隐状态
        /// </summary>
        /// <param name="state"></param>
        public void SetSelectedActive(bool state)
        {
            selectGo.SetActive(state);
            enabled = state;//备注：脚本禁用，Update不执行
            if (state)
                hideTimer = Time.time + 5;
        }

        //选中目标自动消失
        private void Update()
        {
            if (hideTimer <= Time.time)
            {
                SetSelectedActive(false);
            }
        }
    }
}