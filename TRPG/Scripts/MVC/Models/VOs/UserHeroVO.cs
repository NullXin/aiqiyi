using UnityEngine;
using System.Collections;

namespace TRPG.MVC
{
    public class UserHeroVO
    {
        public string userID; // 用户uid
        public string heroID; // 英雄uid
        public string heroName; // 英雄名称
        public string heroType; // 英雄类型
        public int level = 1; // 等级
        public int exp = 100;//经验值
        public int money = 0;// 金币
        public int force = 100;// 力量值
        public int intellence = 100;//智力
        public int speed = 10; // 速度
        public int maxHP = 100;// 最大血值
        public int maxMP = 100; // 最大魔法值
        public int maxDamage = 200;// 最大伤害值

        public override string ToString()
        {
            return "英雄名称:"+heroName+"\n等级:" + level + "\n经验值:" + exp + "\n力量值:"+force + "\n金币:"+money;
        }

        public string[] GetString()
        {
            string[] s = new string[]
            {
                GetStr( this.userID ),
                GetStr( this.heroID ),
                GetStr( this.heroName ),
                GetStr( this.heroType ),
                GetStr( this.level ),
                GetStr( this.exp ),
                GetStr( this.money ),
                GetStr( this.force ),
                GetStr( this.intellence ),
                GetStr( this.speed ),
                GetStr( this.maxHP ),
                GetStr( this.maxMP ),
                GetStr( this.maxDamage )
            };
            return s;
        }

        public string GetStr(object o)
        {
            return "'" + o + "'";
        }
    }

    
}

