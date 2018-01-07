using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TRPG.MVC
{
    public class BasePanel : BaseMonoBehaviour
    {

        protected Button closeButton;
        protected virtual void Awake()
        {
            Transform t = transform.Find( "closeButton" );
            if( t != null )
            {
                closeButton = t.GetComponent<Button>();
                closeButton.onClick.AddListener( Hide );
            }
        }
        /// <summary>
        /// 显示
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive( true );
        }
        /// <summary>
        /// 隐藏
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive( false );
        }
    }
}

