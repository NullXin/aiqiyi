using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TRPG.MVC
{
    /// <summary>
    /// 带有加载进度的场景加载器
    /// </summary>
    public class SceneLoader : BaseMonoBehaviour
    {
        private AsyncOperation async;
        private int percent;
        public Slider proBar;
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene( string scene)
        {
            proBar.gameObject.SetActive( true );
            StartCoroutine( LoadSceneAsync( scene ) );
        }
        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        IEnumerator LoadSceneAsync( string scene)
        {
            percent = 0; // 重置percent
            async = SceneManager.LoadSceneAsync( scene, LoadSceneMode.Additive );
            async.allowSceneActivation = false; // 加载完成之后才自动激活
            yield return async;
        }

        private void Update()
        {
            if (async == null) return;
            //Debug.Log( async.progress ); // async.progress最大为0.9f的坑 
            int toProcess = async.progress < 0.9f ? (int)( async.progress * 100 ) : 100;
            percent = percent < toProcess ? percent + 1 : percent; // percent自增
            Debug.Log( percent );
            proBar.value = percent;
            if( percent == 100)
            {
                proBar.gameObject.SetActive( false );
                async.allowSceneActivation = true; // 加载完成之后自动激活
                async = null;
                SendNotification( NotificationList.LEVEL_LOADED );
            }
        }
        /// <summary>
        /// 异步卸载场景
        /// </summary>
        /// <param name="scene"></param>
        public void UnloadLevel( string scene)
        {
            SceneManager.UnloadSceneAsync( scene );
        }
    }
}

