using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using wzx;
using UnityEngine.SceneManagement;

///<summary>
/// 游戏控制器 : 控制游戏流程，例如游戏开始、暂停、退出。
///</summary>
public class GameController : MonoSingleton<GameController>
{
    private int score;

    public int Score
    {
        get
        {
            return score;
        }
    }

    //添加成绩(敌人死亡时调用)
    public void AddScore(int point)
    {
        score += point;
    }

    //游戏开始前……
    private new void Awake()
    {
        base.Awake();
        SetGunActive(false);
        UIManager.Instance.SetUIActive(UIManager.MAIN_MENU, true);
    }

    //游戏开始……
    //禁用开始界面、激活生成器
    public void GameStart()
    {
        SetGunActive(true);

        //关闭主菜单UI
        UIManager.Instance.SetUIActive(UIManager.MAIN_MENU, false);
        //激活敌人生成器
        SpawnSystem.Instance.ActivateNextSpawn();
    }

    //设置所有枪的激活状态
    private void SetGunActive(bool state)
    {
        foreach (var item in FindObjectsOfType<GunControl>())
        {
            //将枪的激活状态变量，剪切到GunControl。
            item.activeState = state;
        }
    }

    //游戏结束(玩家状态类、生成器系统调用)
    public void GameOver()
    {
        SetGunActive(false);
        //玩家回到原点
        PlayerStatus.Instance.ResetPosition();
        //显示3D键盘 
        UIManager.Instance.SetUIActive(UIManager.KEY_BOARD, true);
    }

    //游戏重新开始
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}