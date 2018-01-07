using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [Tooltip("激活状态")]
    public bool activeState = true;

    [HideInInspector]
    public wzx.Gun gun;
    protected AnimationEventBehaviour animEvent;

    protected void Awake()
    {
        gun = GetComponent<wzx.Gun>();
        animEvent = GetComponentInChildren<AnimationEventBehaviour>();
    }

    protected void OnEnable()
    {
        animEvent.attackHandler += DoFiring; 
    }

    protected void OnDisable()
    {
        animEvent.attackHandler -= DoFiring; 
    }

    protected void DoFiring()
    { 
        gun.Firing(gun.firePointTF.forward);
    }  
}
