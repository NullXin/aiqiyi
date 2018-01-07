using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

///<summary>
/// 排行榜管理器
///</summary>
public class RankingListManager : MonoSingleton<RankingListManager>
{
    private List<RankData> dataList;
    private TextDataService dataService;
    private GameObject itemUIPrefab;
    private Transform contentTF;

    protected override void Initialized()
    {
        base.Initialized();

        dataService = new TextDataService();

        //备注：需要重新生成资源配置文件
        //         修改ResourceManager中读取的配置文件：ResConfig.txt
        itemUIPrefab = ResourceManager.Load<GameObject>("RankItem");
        contentTF = transform.FindChildByName("Content");
    }

    //加载数据
    public void Load()
    {
        dataList = dataService.LoadData();
    }

    //添加数据
    public void AddData(string name)
    {
        RankData data = new RankData()
        {
            Name = name,
            Score = GameController.Instance.Score
        };
        //加到内存中
        dataList.Add(data);
        //加到文件中
        dataService.Append(data);
    }

    //显示面板
    public void Display()
    {
        //显示UI
        gameObject.SetActive(true);

        //排序（按照成绩 升序排列）
        dataList.Sort((a, b) => b.Score.CompareTo(a.Score));

        //创建UI项
        for (int i = 0; i < dataList.Count; i++)
        {
            //i  序号        dataList[i].Name  姓名  dataList[i].Score 成绩
            //创建UI
            RankItem item = Instantiate(itemUIPrefab, contentTF).GetComponent<RankItem>();
            //初始化（修改UI中的文字）
            item.Initialized(i + 1, dataList[i]);
        }
    }
}
