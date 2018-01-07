using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ConfigDic = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;
using Common;
using System;

namespace AI.FSM
{
    ///<summary>
    /// AI 配置文件读取器
    ///</summary>
    public class AIConfigurationReader
    {  
        private static string mainKey;

        public static ConfigDic Load(string aiPath)
        {
            string configFile = ConfigruationReader.GetConfigFile(aiPath);
            ConfigDic map = new ConfigDic(); 
            ConfigruationReader.BuildMap(configFile, map, BuildMapHandler);
            return map;
        }
         
        private static void BuildMapHandler(ConfigDic map, string line)
        {
            //去除空白
            line = line.Trim();
            //跳过空行
            if (string.IsNullOrEmpty(line)) return;
            //[Idle]
            //NoHealth>Dead
            if (line.StartsWith("["))
            {//大字典的键
                mainKey = line.Substring(1, line.Length - 2);
                map.Add(mainKey, new Dictionary<string, string>());
            }
            else
            {//小字典的记录
                string[] configValue = line.Split('>');
                map[mainKey].Add(configValue[0], configValue[1]);
            }
        }
    }
}