using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	///<summary>
	/// 动画事件行为类
	///</summary>
	public class AnimationEventBehaviour : MonoBehaviour
    {
        private Animator anim;

        public event Action attackHandler;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        //由Unity引擎调用
        private void OnCancelAnim(string animParam)
        {
            anim.SetBool(animParam, false);
        }

        //由Unity引擎调用
        private void OnAttack()
        {
            if (attackHandler != null)
            {
                attackHandler();//引发事件
            }
        }
    }
}