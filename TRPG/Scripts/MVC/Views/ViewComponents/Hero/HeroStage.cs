using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace TRPG.MVC
{
    public class HeroStage : BasePanel
    {
        public GameObject heros; // 3d人物
        public Dictionary<string, HeroVO> heroList; // 系统英雄列表

        public SelectableButton wizardButton; // 法师按钮
        public SelectableButton warriorButton; // 战士按钮
        public GameObject wizardHero; // 法师
        public GameObject warriorHero;  // 战士
        public Text infoText; // 英雄信息文本
        public Text nameText; // 名称文本
        private string heroType; // 英雄类型
        public  bool showCloseButton;

        override protected void Awake()
        {
            closeButton = transform.Find( "NamePanel/closeButton" ).GetComponent<Button>();
            closeButton.onClick.AddListener( Hide );
            Button createButton = transform.Find( "NamePanel/createButton" ).GetComponent<Button>();
            createButton.onClick.AddListener( CreateHero );
        }
        /// <summary>
        /// 创建英雄
        /// </summary>
        private void CreateHero()
        {
            if( nameText.text == "")
            {
                Debug.LogError( "名称不能为空!" );return;
            }
            if( StringHelper.CheckBadWord( nameText.text) || !StringHelper.IsSafeSqlString( nameText.text ))
            {
                Debug.LogError( "名称不能有非法字符!" );return;
            }

            UserHeroVO userHero = new UserHeroVO();
            userHero.userID = GameController.instance.crtUser.uid; // 用户uid
            userHero.heroID = Guid.NewGuid().ToString( "N" ); // 英雄uid
            userHero.heroName = nameText.text; // 英雄名称
            userHero.heroType = heroType; // 英雄类型
            SendNotification( NotificationList.CREATE_HERO, userHero );
        }

        /// <summary>
        /// 切换系统英雄
        /// </summary>
        /// <param name="type"></param>
        public void ChangeSystemHero( string type)
        {
            Debug.Log( "ChangeSystemHero: " + type );
            heroType = type;
            switch( type)
            {
                case "Wizard": // 法师
                    wizardButton.selected = true;
                    warriorButton.selected = false;
                    wizardHero.SetActive( true );
                    warriorHero.SetActive( false );
                    break;
                case "Warrior": // 战士
                    wizardButton.selected = false;
                    warriorButton.selected = true;
                    wizardHero.SetActive( false );
                    warriorHero.SetActive( true );
                    break;
            }
            // 更新英雄信息
            HeroVO hero = heroList[type];
            infoText.text = hero.ToString();
        }
        /// <summary>
        /// 显示面板时显示英雄
        /// </summary>
        public override void Show()
        {
            base.Show();
            heros.SetActive( true );
            closeButton.gameObject.SetActive( showCloseButton );
        }
        /// <summary>
        /// 隐藏面板时隐藏英雄
        /// </summary>
        public override void Hide()
        {
            base.Hide();
            heros.SetActive( false );
        }
    }
}

