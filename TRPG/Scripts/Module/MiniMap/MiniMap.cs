using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TRPG.Module
{
    /// <summary>
    /// 小地图功能
    /// </summary>
    public class MiniMap : MonoBehaviour
    {

        private Transform player; // 跟随对象
        private RectTransform mark; // 小箭头
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag( "Player" ).transform;
            mark = transform.Find( "mark" ).GetComponent<RectTransform>();
        }

        private Vector2 tmp;
        private Quaternion rotation;
        void Update()
        {
            //计算Player在xz平面的坐标换算为UI上xy平面的坐标
            tmp.x = player.position.x * 2;
            tmp.y = player.position.z * 2;
            mark.anchoredPosition = tmp;
            // 将Player在xz平面的旋转角度  换算为UI上xy平面的旋转角度

            rotation = Quaternion.Euler(
                player.rotation.eulerAngles.x,
                player.rotation.eulerAngles.z,
                -player.rotation.eulerAngles.y );
            mark.rotation = rotation;
        }
    }
}

