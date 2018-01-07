using UnityEngine;
using System.Collections;

namespace TRPG.Module
{
    /// <summary>
    /// 玩家移动时动画状态的切换
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        private Animation animation;
        private void Awake()
        {
            animation = GetComponent<Animation>();
            // 获取Joystick, 添加监听事件
            ETCJoystick moveStick = GameObject.FindObjectOfType<ETCJoystick>();
            moveStick.onMoveStart.AddListener( OnMoveStart );
            moveStick.onMove.AddListener( OnMove );
            moveStick.onMoveEnd.AddListener( OnMoveEnd );
        }
        /// <summary>
        /// 开始移动
        /// </summary>
        public void OnMoveStart()
        {
            animation.CrossFade( "run" );
        }
        /// <summary>
        /// 持续移动
        /// </summary>
        /// <param name="speed"></param>
        public void OnMove( Vector2 speed)
        {
            animation.Play( "run" );
        }
        /// <summary>
        /// 停止移动
        /// </summary>
        public void OnMoveEnd()
        {
            animation.CrossFade( "idle" );
        }
    }
}

