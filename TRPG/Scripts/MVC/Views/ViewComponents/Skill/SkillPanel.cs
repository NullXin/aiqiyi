using UnityEngine;
using System.Collections;
using TRPG.Module;
using UnityEngine.UI;

namespace TRPG.MVC
{
    public class SkillPanel : BasePanel
    {
        public SkillInfo[] skills; // 所有技能
        public SkillItem skillItemPrefab; // 技能Item
        public Text infoText; // 信息
        public Transform content; // 技能Item的容器
        public Button[] skillButtons;// 技能按钮

        private void Start()
        {
            Debug.Log( "SkillPanel::Start " + skills.Length );
            // 移除content现有HeroItem
            for (int i = content.childCount - 1 ; i >= 0 ; i--)
            {
                Destroy( content.GetChild( i ).gameObject );
            }
            // 遍历list
            for (int i = 0 ; i < skills.Length ; i++)
            {
                // 实例化HeroItem
                SkillItem item = Instantiate( skillItemPrefab, content );
                item.SetData( skills[i] as SkillInfo );
                // 添加点击事件
                item.GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        Select( item );
                    } );
                // x,y
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2( 94, -54 - 105 * i );
                // 自动选中第一个
                if (i == 0) Select( item );
            }

            // 设置scroll view content 的高度
            content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, 105 * skills.Length + 20 );

            // 设置技能
            for (int i = 0 ; i < skillButtons.Length ; i++) // 所有技能按钮
            {
                Button b = skillButtons[i];
                b.name = i + "";
                b.onClick.AddListener( delegate ()
                {
                    SetSkill( b );  // 点击时设置技能
                } );
            }

        }

        public SkillItem crtItem = null; // 当前选中Item
        private void Select(SkillItem item) // 点击Item时
        {
            if (crtItem != null) // 如果当前有选中的Item
                crtItem.selected = false; // 则切换为不选中
            item.selected = true; // 点击的Item 选中
            crtItem = item; // 保存当前点击的Item

            infoText.text = crtItem.skillData.ToString();
        }
        /// <summary>
        /// 保存技能设置到PlayerPrefs中
        /// </summary>
        /// <param name="btn"></param>
        private void SetSkill( Button btn)
        {
            Image img = btn.transform.Find( "icon" ).GetComponent<Image>();
            img.sprite = crtItem.skillData.icon; // 设置图标为技能图标
            img.rectTransform.sizeDelta = new Vector2( 50, 50 ); // 设置图标大小

            PlayerPrefs.SetString( "s" + btn.name, crtItem.skillData.name );
            print( PlayerPrefs.GetString( "s0" ) + " " + PlayerPrefs.GetString( "s1" ) );
        }
    }
}

