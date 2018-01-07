using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
/// 附加到排行榜项中，负责初始化每项内容
///</summary>
public class RankItem : MonoBehaviour
{
    public void Initialized(int id, RankData data)
    {
        transform.FindChildByName("TextId").GetComponent<Text>().text = id.ToString();
        transform.FindChildByName("TextName").GetComponent<Text>().text = data.Name;
        transform.FindChildByName("TextScore").GetComponent<Text>().text = data.Score.ToString();
    }
}