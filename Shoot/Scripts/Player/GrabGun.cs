using UnityEngine;
using System.Collections;
using Tarena.Lansquenet;

public class GrabGun : VRTK.VRTK_InteractableObject
{ 
    //抓取时执行
    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
         
        StartCoroutine(GrabingGun());
    } 
    
    private IEnumerator GrabingGun()
    {
        yield return 0;
        this.GetComponent<Tarena.Lansquenet.HtcGunControl>().enabled = true;
    }
}
