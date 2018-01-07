using UnityEngine;
using System.Collections;
using NodeCanvas.Framework;

namespace TRPG.Module
{
    public class Health : MonoBehaviour
    {
        private float hp = 100; // 血值
        /// <summary>
        /// 承受伤害
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage( float damage)
        {
            
            hp -= damage;
            print( "TakeDamage: " + damage + " hp: " + hp);
            if ( gameObject.tag == "Monster")
            {
                Blackboard bb = gameObject.GetComponent<Blackboard>();
                bb.SetValue( "hp", hp );
               // bb.GetValue<float>("hp")
            }
        }
    }
}

