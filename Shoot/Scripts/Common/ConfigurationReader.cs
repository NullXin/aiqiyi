using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 配置文件读取器
/// </summary>
public class ConfigurationReader
{
    /// <summary>
    /// 获取配置文件
    /// </summary>
    /// <param name="path">StreamingAssets的相对目录</param>
    /// <returns></returns>
    public static string GetConfigFile(string path)
    {
        string configFile = Application.streamingAssetsPath + "/" + path;

        if (Application.platform != RuntimePlatform.Android)
            configFile = "file://" + configFile; 

        WWW www = new WWW(configFile);
        while (true)
        {
            if (www.isDone)
                return www.text;
        }
    }

    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <typeparam name="T">配置文件的数据结构</typeparam>
    /// <param name="configFileContent">配置文件内容</param>
    /// <param name="data">需要加载到的对象</param>
    /// <param name="lineHandler">处理逻辑</param>
    public static void Load<T>(string configFileContent, T data,Action<T,string> lineHandler)
    { 
        using (StringReader reader = new StringReader(configFileContent))
        {
            string line = null;
            while ((line = reader.ReadLine()) != null)
            { 
                lineHandler(data, line);
            }
        }
    }
}
