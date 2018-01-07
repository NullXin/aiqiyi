using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ConfigDic = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;

namespace AI.FSM
{
    public static class AIConfigurationReader
    {
        /// <summary>
        /// 配置文件存储的目录
        /// </summary>
        public const string PATH = "Config";

        private static string mainKey = null;//主键
        private static string subKey = null;//子键
        private static string subValue = null;//值

        public static ConfigDic Load(string aiConfigFile)
        {
            string configFile = ConfigurationReader.GetConfigFile(PATH + "/" + aiConfigFile);
            ConfigDic dic = new ConfigDic();
            ConfigurationReader.Load(configFile, dic, BuildMap);
            return dic;

            //aiConfigFile = string.Format("{0}/{1}/{2}", Application.streamingAssetsPath, PATH, aiConfigFile);
            //if (Application.platform != RuntimePlatform.Android)
            //        aiConfigFile = "file://" + aiConfigFile;

            //aiConfigFile = string.Format("file://{0}/{1}/{2}", Application.streamingAssetsPath, PATH, aiConfigFile);
            //#if !UNITY_EDITOR && UNITY_ANDROID
            //           aiConfigFile = aiConfigFile.Substring(7);
            //#endif

            //#if UNITY_EDITOR
            //            aiConfigFile = "file://" + Application.dataPath + "/StreamingAssets/" + PATH + "/" + aiConfigFile;
            //#elif UNITY_IPHONE
            //              aiConfigFile = "file://" + Application.dataPath + "/Raw/" + PATH + "/" + aiConfigFile;
            //#elif UNITY_ANDROID
            //               aiConfigFile = "jar:file://" + Application.dataPath + "!/assets/" + PATH + "/" + aiConfigFile;
            //#endif 

            //WWW www = new WWW(aiConfigFile);
            //while (true)
            //{ 
            //    if (www.isDone)
            //        return BuildDic(www.text);
            //}

            //string configFile = ResourceManager.GetConfigFile(PATH + "/" + aiConfigFile);
            //return BuildDic(configFile); 
        }
        
        private static void BuildMap(ConfigDic map, string line)
        {
            line = line.Trim();//去除空白行
            if (!string.IsNullOrEmpty(line))
            {
                //取主键
                if (line.StartsWith("["))
                {
                    mainKey = line.Substring(1, line.IndexOf("]") - 1);
                    map.Add(mainKey, new Dictionary<string, string>());
                }
                //取子键以及值
                else
                {
                    var configValue = line.Split('>');
                    subKey = configValue[0].Trim();
                    subValue = configValue[1].Trim();
                    map[mainKey].Add(subKey, subValue);
                }
            }
        }

        /// <summary>
        /// 处理所有数据
        /// </summary>
        //private static ConfigDic BuildDic(string lines)
        //{
        //    ConfigDic dic = new ConfigDic();
        //    string mainKey = null;//主键
        //    string subKey = null;//子键
        //    string subValue = null;//值
        //    StringReader reader = new StringReader(lines);
        //    string line = null;
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        line = line.Trim();//去除空白行
        //        if (!string.IsNullOrEmpty(line))
        //        {
        //            //取主键
        //            if (line.StartsWith("["))
        //            {
        //                mainKey = line.Substring(1, line.IndexOf("]") - 1);
        //                dic.Add(mainKey, new Dictionary<string, string>());
        //            }
        //            //取子键以及值
        //            else
        //            {
        //                var configValue = line.Split('>');
        //                subKey = configValue[0].Trim();
        //                subValue = configValue[1].Trim();
        //                dic[mainKey].Add(subKey, subValue);
        //            }
        //        }
        //    }
        //    return dic;
        //}
    }
}


