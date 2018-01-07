using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TRPG.MVC
{
    public class HeroItem : MonoBehaviour
    {
        public Text nameText;
        public Text lvText;

        public UserHeroVO heroData;
        public void SetData( UserHeroVO hero)
        {
            heroData = hero;
            nameText.text = hero.heroName;
            lvText.text = "等级:" + hero.level;
        }
        public GameObject selectBorder;
        public bool selected
        {
            set { selectBorder.SetActive( value ); }
        }

    }
}

