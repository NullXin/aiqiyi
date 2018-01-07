using UnityEngine;
using System.Collections;
using VRTK;
using UnityEngine.UI;

namespace Tarena.Lansquenet
{
    /// <summary>
    /// 单发枪
    /// </summary>
    public class HtcSingleGunControl : HtcGunControl
    {    
        protected new void OnEnable()
        {
            base.OnEnable();
            controllerEvent.TriggerClicked += Controller_TriggerClicked;
        }

        protected new void OnDisable()
        {
            base.OnDisable();
            controllerEvent.TriggerClicked -= Controller_TriggerClicked;
        }

        //发射子弹
        private void Controller_TriggerClicked(object sender, ControllerInteractionEventArgs e)
        {
            if (!activeState) return;
            gun.anim.SetBool(gun.fireAnimName, true);
            base.Pulse();
        }
    }
}