using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRTK;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class GrabGun :VRTK_InteractableObject
	{ 
        //当枪被握住后，启用枪的射击功能
        public override void Grabbed(GameObject currentGrabbingObject)
        {    
            base.Grabbed(currentGrabbingObject);
            print("启用枪的射击功能");
        }
    }
}