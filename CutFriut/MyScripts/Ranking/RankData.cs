using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//命名空间
namespace Rank
{
	/// <summary>
	/// 排名数据
	/// </summary>
    [System.Serializable]
	public class RankData : IComparer<RankData>
    {
        public string name;

        public float score;

        public int Compare(RankData x, RankData y)
        {
            return x.score.CompareTo(y.score);
        }
    }

    public class RankDatas {
        public List<RankData> ranks;

        public RankDatas() {
            ranks = new List<RankData>();
        }
    }
}
