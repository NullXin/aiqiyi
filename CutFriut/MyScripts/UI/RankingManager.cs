using UnityEngine;
using Common;
using Rank;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using wzx;
using VRTK;

/// <summary>
/// 
/// </summary>
public class RankingManager :  MonoSingleton<RankingManager>
{
    public string fileName;
    private RankDatas RankData;
    private GameObject rankItemPrefab;
    private Button resetButton;
    private Transform content;
	public VRTK_UIPointer uipointer;

    private void Start()
    {
        RankData = new RankDatas();
        print(RankJson.Instance.gameObject.name);
        RankJson.Instance.InitFilePath(fileName);
        rankItemPrefab = Resources.Load<GameObject>("UI/RankingItem");
        content = transform.FindChildByName("Content");
		uipointer.SetWorldCanvas (GetComponent<Canvas>());
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        resetButton = transform.FindChildByName("ButtonClose").GetComponent<Button>();

        resetButton.onClick.AddListener(RestGame);
    }

    private void OnDisable()
    {
        resetButton.onClick.RemoveListener(RestGame);
    }

    //重新开始游戏
    private void RestGame()
    {
        SceneManager.LoadScene(0);
    }

    //显示排行榜
    public void DisplayRanking(string useName) {
        gameObject.SetActive(true);

        SaveData(useName);

        ShowRankUIData();
    }

    //显示界面排行榜数据
    private void ShowRankUIData()
    {
        RankData = RankJson.GetRankingObject<RankDatas>();
        RankData[] datas = RankData.ranks.ToArray();
        datas.OrderByDescending(o => o.score);
        CalculationContentSize(datas.Length);

        for (int i = 0; i < datas.Length; i++)
        {
            CreateRankingItem(datas[i],i+1);
        }
    }

    //计算Content大小
    private void CalculationContentSize(int count) {
        Vector2 size = (content as RectTransform).sizeDelta;
        size.y = 40 * count;
        (content as RectTransform).sizeDelta = size;
    }

    //创建排名项
    private void CreateRankingItem(RankData data,int rank) {
        GameObject item = Instantiate(rankItemPrefab,content);
        item.transform.FindChildByName("TextID").GetComponent<Text>().text = rank.ToString();
        item.transform.FindChildByName("TextName").GetComponent<Text>().text = data.name.ToString();
        item.transform.FindChildByName("TextScore").GetComponent<Text>().text = data.score.ToString();
    }

    //保存数据
    private void SaveData(string useName)
    {
        RankData data = new RankData()
        {
            name = useName,
            score = GameManager.Instance.TotalScore,
        };

        RankData.ranks.Add(data);
        RankJson.SaveRanking(RankData);
    }
}
