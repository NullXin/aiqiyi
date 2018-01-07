using UnityEngine;
using System.Collections;

namespace wzx
{
    /// <summary>
    /// 主角子弹 
    /// </summary>
    public class PlayerBullet : Bullet
    {  
        protected override void ArriveTargetPoint()
        {
            base.ArriveTargetPoint();

            //攻击敌人
            if (hit.collider != null && hit.collider.tag == Tags.ENEMY)
                hit.collider.GetComponentInParent<EnemyStatus>().Damage(CalculateAttackForce());
        }

        private float CalculateAttackForce()
        {
            switch (hit.collider.name)
            {
                case "Coll_Head":
                    return atk * 2;
                case "Coll_Body":
                    return atk;
                default://胳膊、推
                    return atk * 0.5f;
            }
        }
    }
}