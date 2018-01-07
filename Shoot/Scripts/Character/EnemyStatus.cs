using UnityEngine; 

namespace wzx
{
    /// <summary>
    /// 敌人状态类
    /// </summary>
    public class EnemyStatus : CharacterStatus 
    { 
        [Tooltip("动画参数")]
        public EnemyAnimationParameter animParams;
         
        [Tooltip("死亡延迟时间")]
        public float deathDelay = 5;

        [Tooltip("被消灭后的得分")]
        public int point = 10;
         
        protected override void Death()
        {
            base.Death();
             
            GetComponentInChildren<Animator>().SetBool(animParams.death, true);
               
            Destroy(gameObject, deathDelay); 
              
            GameController.Instance.AddScore(point);
        } 
    }
}