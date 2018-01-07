using UnityEngine;
using System.Collections;
using Common;

/*
 不同武器使用方式不同
 武器都要准备、释放
     */
namespace wzx
{
    /// <summary>
    /// 枪
    /// </summary> 
    [RequireComponent(typeof(AudioSource),typeof(MuzzleFlash))]
    public class Gun : MonoBehaviour
    {  

        [Tooltip("开火点名称")]
        public string firePointName = "FirePoint";
        /// <summary>
        /// 开火点
        /// </summary>
        [HideInInspector]
        public Transform firePointTF;
        [Tooltip("子弹预制件名称")]
        public string bulletPrefabName = "PlayerBullet";
        private GameObject bulletPrefab;  
        private AudioSource audioSource; 
        private MuzzleFlash muzzleFire;
        [Tooltip("开火动画名称")]
        public string fireAnimName = "Fire";
        [HideInInspector]
        public Animator anim;
        [Tooltip("攻击力")]
        public float atk = 50;
        protected void Awake()
        {  
            InitComponent();

            bulletPrefab = ResourceManager.Load<GameObject>(bulletPrefabName); 
        }

        private void InitComponent()
        {
            audioSource = GetComponent<AudioSource>();
            anim = GetComponentInChildren<Animator>(); 
            muzzleFire = GetComponent<MuzzleFlash>();  
            firePointTF = transform.FindChildByName(firePointName);  
        } 
         
        //枪发射(敌人：从敌人枪口指向玩家头部。  玩家：枪口正前方)
        public void Firing(Vector3 direction)
        {  
            GameObject go = Instantiate(bulletPrefab, firePointTF.position, Quaternion.LookRotation(direction)) as GameObject;
           
            //初始化 传递参数
            go.GetComponent<Bullet>().Init(atk);//子弹位置   子弹方向

            //播放声音
            audioSource.Play();

            //显示火花
            muzzleFire.DisplayFlash();  
        }   
    }
}