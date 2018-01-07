using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TRPG.MVC
{
    /// <summary>
    /// 点击时发送消息的按钮
    /// </summary>
    [RequireComponent( typeof(Button) )]
    public class NotiButton : BaseMonoBehaviour
    {
        /// <summary>
        /// 消息名称
        /// </summary>
        public string notificationName;
        /// <summary>
        /// 消息数据
        /// </summary>
        public string data;
        // Use this for initialization
        void Awake()
        {
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(
                delegate
                {
                    SendNotification( notificationName, data );
                }
            );
        }

    }
}

