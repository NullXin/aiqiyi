using System.Collections.Generic;
using UnityEngine;
using System.IO;
 
public class ResourceManager : MonoBehaviour
{
    private static Dictionary<string, string> configMap;

    private static Dictionary<string, Object> resCache;

    static ResourceManager()
    {
        configMap = new Dictionary<string, string>();
        resCache = new Dictionary<string, Object>();
        LoadConfig();    
    }

    public const string FILE_NAME = "ResConfig.txt";

    /// 加载资源映射文件 
    private static void LoadConfig()
    {    
        string mapText = ConfigurationReader.GetConfigFile(FILE_NAME);
         
        ConfigurationReader.Load(mapText, configMap, BuildMap); 
    }

    private static void BuildMap(Dictionary<string, string> map,string line)
    {
        var keyValue = line.Split('='); 
        map.Add(keyValue[0], keyValue[1]);
    }

    //通过资源名取得资源路径 
    private static string GetValue(string resName)
    {
        if (configMap.ContainsKey(resName))
            return configMap[resName];
        return null;
    }

    //获取资源
    private static T GetResource<T>(string relativePath) where T : Object
    {
        //如果资源缓存对象中不存在
        if (!resCache.ContainsKey(relativePath))
        {
            T obj = Resources.Load<T>(relativePath);
            if (!obj) return null;//如果没有该资源 返回空
            resCache.Add(relativePath, obj);
            return obj;
        }
        return resCache[relativePath] as T;
    }

    /// <summary>
    /// 在Resources文件夹中加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="resName">资源名称</param>
    /// <returns></returns>
    public static T Load<T>(string resName) where T : Object
    {
        string path = GetValue(resName);
        if (string.IsNullOrEmpty(path)) return null;//如果配置文件没有该记录 返回空
        return GetResource<T>(path);
    } 
}
