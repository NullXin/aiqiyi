using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	///<summary>
	/// 用户界面管理器
	///</summary>
	public class UIManager : MonoSingleton<UIManager>
	{
        #region 管理所有UI名称
        /// <summary>
        /// 主菜单名称
        /// </summary>
        public const string MAIN_MENU = "CW_MainMenu";

        /// <summary>
        /// 键盘名称
        /// </summary>
        public const string KEY_BOARD = "CW_Keyboard";

        /// <summary>
        /// 排行榜名称
        /// </summary>
        public const string RANKING_LIST = "CW_RankingList";
        #endregion
         
        //2.设置UI显隐的容器
        private Dictionary<string, GameObject> uiDic;

        //手动添加一个VRTK_UIPointer
        public VRTK.VRTK_UIPointer uiPointer;//[需要在编译器中拖拽]

        //完成初始化工作
        protected override void Initialized()
        {
            base.Initialized();

            //查找所有WordUI界面
            var allUI = GameObject.FindGameObjectsWithTag(Tags.CANVAS_WORLD);
            uiDic = new Dictionary<string, GameObject>();

            //遍历所有
            for (int i = 0; i < allUI.Length; i++)
            {
                uiDic.Add(allUI[i].name, allUI[i]);
                //将当前画布设置为VRTK画布（在vr中非常重要！）
                //uiPointer.SetWorldCanvas(allUI[i].GetComponent<Canvas>());
                //禁用所有UI
                allUI[i].SetActive(false);
            }
        }

        /// <summary>
        /// 设置UI激活状态
        /// </summary>
        /// <param name="uiName">UI名称</param>
        /// <param name="state">状态</param>
        public void SetUIActive(string uiName,bool state)
        { 
            var panel = GetUIByName(uiName);
            if (panel != null)
                panel.SetActive(state);
        }

        /// <summary>
        /// 根据名称获取UI
        /// </summary>
        /// <param name="uiName">名称</param>
        /// <returns></returns>
        public GameObject GetUIByName(string uiName)
        { 
            return uiDic.ContainsKey(uiName) ? uiDic[uiName] : null;
        }

    }
}