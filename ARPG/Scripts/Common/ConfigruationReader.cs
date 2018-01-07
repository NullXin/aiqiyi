using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common
{
    ///<summary>
    /// 配置文件读取器
    ///</summary>
    public class ConfigruationReader
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetConfigFile(string filePath)
        {
            //构建路径
            string configPath = Application.streamingAssetsPath + "/"+ filePath;
            if (Application.platform != RuntimePlatform.Android)
                configPath = "file://" + configPath;

            //加载文件
            WWW www = new WWW(configPath);
            while (true)
            {
                //是否加载完成
                if (www.isDone)
                    return www.text;
            }
        }
          
        /// <summary>
        /// 解析配置文件
        /// </summary>
        /// <typeparam name="T">配置文件数据结构</typeparam>
        /// <param name="configFile">配置文件字符串</param>
        /// <param name="map">配置文件读取后的容器</param>
        /// <param name="handler">对于每行的处理逻辑</param>
        public static void BuildMap<T>(string configFile,T map,Action<T,string> handler)
        {  
            //字符串读取器：逐行读取
            StringReader reader = new StringReader(configFile);
             
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                handler(map, line); 
            }
        }
    }
}