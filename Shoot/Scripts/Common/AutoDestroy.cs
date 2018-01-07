using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{ 
    public float delayTime = 5;

    private void Start()
    { 
            Destroy(gameObject, delayTime);
    }

}
