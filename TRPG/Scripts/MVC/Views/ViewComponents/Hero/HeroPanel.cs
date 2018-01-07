using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TRPG.MVC
{
	///<summary>
	///
	///</summary>
	public class HeroPanel:BasePanel
	{
        public HeroItem heroItemPrefab;
        public Transform content;
        public Text infoText;
        public void UpdateUserHero( ArrayList heroList)
        {
            Debug.Log( "HeroPanel::UpdateUserHero " + heroList.Count );
            // 移除content现有HeroItem
            for (int i = content.childCount - 1 ; i >= 0 ; i--)
            {
                Destroy( content.GetChild( i ).gameObject );
            }
            // 遍历list
            for (int i = 0 ; i < heroList.Count ; i++)
            {
                // 实例化HeroItem
                HeroItem item = Instantiate( heroItemPrefab, content );
                item.SetData( heroList[i] as UserHeroVO );
                // 添加点击事件
                item.GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        Select( item );
                    } );
                // x,y
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2( 94, -54 - 105 * i );
            }

            // 设置scroll view content 的高度
            content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, 105 * heroList.Count + 20 );
        }

        public HeroItem crtItem = null; // 当前选中Item
        private void Select( HeroItem item) // 点击Item时
        {
            if (crtItem != null) // 如果当前有选中的Item
                crtItem.selected = false; // 则切换为不选中
            item.selected = true; // 点击的Item 选中
            crtItem = item; // 保存当前点击的Item

            infoText.text = crtItem.heroData.ToString();
        }
        public override void Show()
        {
            base.Show();
            SendNotification( NotificationList.GET_USER_HERO );
        }
    }
}