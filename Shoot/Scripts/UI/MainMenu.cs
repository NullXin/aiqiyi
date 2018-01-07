using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using wzx;

namespace ns
{
	///<summary>
	///
	///</summary>
	public class MainMenu : MonoBehaviour
	{
        private void Start()
        {
            transform.FindChildByName("ButtonStart").GetComponent<Button>().onClick.AddListener(GameController.Instance.GameStart);
        }  
    }
}