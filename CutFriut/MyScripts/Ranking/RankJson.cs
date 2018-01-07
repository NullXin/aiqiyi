using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.IO;
using System.Text;

//命名空间
namespace Rank
{
	/// <summary>
	/// 
	/// </summary>
	public class RankJson : MonoSingleton<RankJson>
    {
        private static string filePath;   //排名文件路径

        public void InitFilePath(string fileName){
            filePath = Application.dataPath + "/" + fileName+".json";
            print("file:"+filePath);
        }

        /// 对象转json
        public static string ObjectToJson(object ob) {
            return JsonUtility.ToJson(ob);
        }

        /// <summary>
        /// 字符串转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>对象</returns>
        public static T JsonToObject<T>(string json)
        {
            T data = JsonUtility.FromJson<T>(json);

            return data != null ? data : default(T);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ob"></param>
        public static void SaveRanking<T>(T ob)
        {
            string newjson = ObjectToJson(ob);

            if (!File.Exists(filePath))  // 判断是否已有相同文件 
            {
                print(filePath);
                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                file.Close();
            }

            string json = StringHandler(newjson);

            File.WriteAllText(filePath, json + "\n", Encoding.UTF8);
        }

        //字符串处理
        private static string StringHandler(string newjson)
        {
            string json = ReadFile();

            if (json != string.Empty)
            {

                newjson = newjson.Remove(0, newjson.IndexOf("[") + 1);

                newjson = "," + newjson;

                newjson = newjson.Remove(newjson.IndexOf("]"), newjson.Length - newjson.IndexOf("]"));

                json = json.Insert(json.LastIndexOf(']'), newjson);
            }
            else
                json = newjson;
            return json;
        }

        /// <summary>
        /// 获取排行数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRankingObject<T>() {
            return JsonToObject<T>(ReadFile());
        }

        //读文件
        private static string ReadFile() {

            if (!File.Exists(filePath))  // 判断文件 是否存在
            {
                return "";
            }

            StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

            string str = sr.ReadToEnd();

            sr.Close();

            return str;
        }
    }
}
