using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common
{
    ///<summary>
    /// 资源管理类：提供Resource加载资源的相关功能
    ///</summary>
    public class ResourceManager
	{
        private static Dictionary<string, string> resMap;

        //提供加载资源的方法
        static ResourceManager()
        {
            Load();
        }

        private static void Load()
        {
            resMap = new Dictionary<string, string>();
            string configFile = ConfigruationReader.GetConfigFile("Config.txt");
            ConfigruationReader.BuildMap(configFile, resMap, BuildMapHandler);
        }

        private static void BuildMapHandler(Dictionary<string, string> dic, string line)
        {
            string[] keyValue = line.Split('=');
            dic.Add(keyValue[0], keyValue[1]);
        }

        public static T Load<T>(string resName) where T : UnityEngine.Object
        {
            if (!resMap.ContainsKey(resName)) return null;
            //根据资源名称 查找路径
            string path = resMap[resName];
            //加载资源
            return Resources.Load<T>(path);
        }
	}
}