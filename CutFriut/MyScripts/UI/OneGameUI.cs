using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wzx {

    /// <summary>
    /// One Game
    /// xiaoxin
    /// </summary>
    public class OneGameUI : BaseUI {
        private Text TimeText;
        private Text ScoreText;

         void Start () {
            TimeText = FindChildByName (transform, "TimeText").GetComponent<Text> ();
            ScoreText = FindChildByName (transform, "ScoreText").GetComponent<Text> ();
            print(CountTime);
            TimeText.text = "时间:" + GameManager.Instance.FormatTime (CountTime);
        }

        void FixedUpdate () {
            if (IsStart) {
                //倒计时实现
                RemainTime = CountTime - (Time.time - StartTime);

                if (RemainTime <= 0) {
                    //显示特效
                    UIManager.Instance.ChangeUI();
                    TimeText.text = "时间:" + GameManager.Instance.FormatTime (0);
                    return;
                }

                TimeText.text = "时间:" + GameManager.Instance.FormatTime (RemainTime);
            }
        }

        /// <summary>
        /// 更新UI
        /// </summary>
        protected override void UpdateUI () {
            ScoreText.text = "分数：" + GameManager.Instance.TotalScore;
        }
    }
}