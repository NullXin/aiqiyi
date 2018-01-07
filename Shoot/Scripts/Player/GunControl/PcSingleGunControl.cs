using UnityEngine;

[RequireComponent(typeof(wzx.Gun))]
public class PcSingleGunControl : GunControl
{
    private float lastClickTime;
    public float minFireInterval = 0.2f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.unscaledTime - lastClickTime < minFireInterval) return;

            if (!activeState) return; 

            gun.anim.SetBool(gun.fireAnimName, true);

            lastClickTime = Time.unscaledTime;
        } 
    }
}
