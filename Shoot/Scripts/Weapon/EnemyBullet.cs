using UnityEngine;
using System.Collections;

namespace wzx
{
    public class EnemyBullet : Bullet
    {
        //与环境做射线检测
        //与主角做检测碰撞 
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.MAINCAMREA || other.tag==Tags.PLAYER)
            {
                //攻击玩家
                PlayerStatus.Instance.Damage(atk); 
                Destroy(gameObject);
            }   
        }
    }
}