using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRPG.MVC;
using System;


	///<summary>
	/// 整个程序的入口文件
	///</summary>
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject[] userRelativeObjects; // 和用户系统相关的对象
    public GameObject[] mainRelativeObjects; // 和主界面相关的对象
    public GameObject[] gameRelativeObjects; // 和游戏相关的对象
    public Transform mainCanvas;

    public UserVO crtUser; // 当前用户(current)
    private void Start()
    {
        // 限制帧频为30
        Application.targetFrameRate = 30;
        // 永不销毁
        DontDestroyOnLoad( this.gameObject );
        instance = this;

        ApplicationFacade facade = ApplicationFacade.GetInstance();
        facade.Start();
    }
    /// <summary>
    /// 获取面板
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Transform GetChild( string name)
    {
        return instance.mainCanvas.Find( name );
    }
}
