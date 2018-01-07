using UnityEngine;
using System.Collections;

namespace TRPG.MVC
{
    public class HeroVO
    {
        public string id;
        public string uid; 
        public string type; // 类型
        public string name; // 名称
        public string weapon; // 武器
        public string role; // 职业
        public string description; //描述信息

        public override string ToString()
        {
            return "名字:" + name + "\n描述信息:" + description;
        }
    }
}

