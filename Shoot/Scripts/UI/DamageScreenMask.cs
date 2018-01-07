using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;

namespace wzx
{
	/// <summary>
	/// 玩家受伤害受屏幕的影响
	/// </summary>
	public class DamageScreenMask : MonoSingleton<DamageScreenMask>
	{
        [Tooltip("渐变时间")]
        public float Delay=2;
        [Tooltip("渐变方式")]
        public AnimationCurve curve;
        private Image effectImage;
        [Tooltip("渐变色彩")]
        public Color originalColor;
        private Color targetColor = Color.clear;

        protected override void Initialized()
        {
            base.Initialized();
            effectImage=GetComponent<Image>();
        }
        //屏幕受影响
        public void DamageScreenEffect()
        {
            targetColor = PlayerStatus.Instance.demageColor;
            StartCoroutine(EffectFade());
        }

        IEnumerator EffectFade()
        {
            for (float i = 0; i < Delay; i+=Time.deltaTime)
            {
                effectImage.color = Color.LerpUnclamped(originalColor, targetColor, i);
                yield return null;
            }
            Color c = effectImage.color;
            c.a = 0;
            effectImage.color = c;
        }

    }
}