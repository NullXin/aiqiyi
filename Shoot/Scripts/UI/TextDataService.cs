using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

///<summary>
/// 文本文件数据服务类
///</summary>
public class TextDataService
{
    private string path = Application.streamingAssetsPath + "/Rank.txt";

    public TextDataService()
    {
        if (!File.Exists(path))
        {
            File.Create(path);
        }
    }
     
    public List<RankData> LoadData()
    {
        List<RankData> result = new List<RankData>();
        //数据：姓名  成绩 ……
        //创建文件流
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        //创建流的读取器
        using (StreamReader reader = new StreamReader(stream))
        {
            //读一行
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] dataArray = line.Split('#');
                result.Add(new RankData()
                {
                    Name = dataArray[0],
                    Score = int.Parse(dataArray[1])
                });
            }
            return result;
        }
    }

    public void Append(RankData data)
    {
        //创建文件流
        FileStream stream = new FileStream(path, FileMode.Append);
        //创建流的写入器
        using (StreamWriter writer = new StreamWriter(stream))
        {             
            //姓名#成绩
            writer.WriteLine("{0}#{1}", data.Name, data.Score);
        }
    }
}