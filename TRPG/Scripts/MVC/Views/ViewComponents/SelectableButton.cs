using UnityEngine;
using System.Collections;

namespace TRPG.MVC
{
    public class SelectableButton : MonoBehaviour
    {
        public GameObject selectBorder; 
        private bool _selected = false;
        public bool selected
        {
            set { _selected = value;
                if (selectBorder != null) selectBorder.SetActive( value );
            }
            get { return _selected; }
        }
       
    }
}

