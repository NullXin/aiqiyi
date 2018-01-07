using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TRPG.Module;

namespace TRPG.MVC
{
    public class SkillItem : MonoBehaviour
    {
        public Text nameText;
        public Text lvText;
        public Image icon;

        public SkillInfo skillData;
        public void SetData(SkillInfo skill)
        {
            skillData = skill;
            nameText.text = skill.name;
            icon.sprite = skill.icon;
            //lvText.text = "等级:" + hero.level;
        }
        public GameObject selectBorder;
        public bool selected
        {
            set { selectBorder.SetActive( value ); }
        }

    }
}

