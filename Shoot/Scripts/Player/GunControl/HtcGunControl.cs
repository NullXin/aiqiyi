using UnityEngine;
using System.Collections;
using VRTK;

namespace Tarena.Lansquenet
{
    [RequireComponent(typeof(Gun))]
    public class HtcGunControl : GunControl
    {
        /// <summary>
        /// 震动力度
        /// </summary>
        public ushort strength = 3000;
        /// <summary>
        /// 持续时间
        /// </summary>
        public float duration = 0.05f;
        /// <summary>
        /// 间隔
        /// </summary>
        public float interval = 0.02f;
         
        private VRTK_ControllerActions controllerAction;
        protected VRTK_ControllerEvents controllerEvent;

        protected new void OnEnable()
        {
            base.OnEnable();

            controllerEvent = GetComponentInParent<VRTK_ControllerEvents>();
            controllerAction = GetComponentInParent<VRTK_ControllerActions>(); 
        } 
          
        /// <summary>
        /// 手柄震动
        /// </summary>
        protected void Pulse()
        {
            controllerAction.TriggerHapticPulse(strength, duration, interval); 
        }
    }
}