using UnityEngine;
using UnityEngine.UI;

public class UI_Keyboard : MonoBehaviour {
    private InputField input;

    public void ClickKey(string character)
    {
        input.text += character;
    }

    public void Backspace()
    {
        if (input.text.Length > 0)
        {
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }

    public void Enter()
    {
        Debug.Log("You've typed [" + input.text + "]");
        //input.text = "";

        //备注：将 UI_Keyboard 脚本移动到Scripts文件夹内

        //加载排行榜数据
        RankingListManager.Instance.Load();

        //添加新纪录(姓名  成绩)
        RankingListManager.Instance.AddData(input.text);

        //显示排行榜      
        RankingListManager.Instance.Display();

        //禁用当前面板
        gameObject.SetActive(false);

    }

    private void Start()
    {
        input = GetComponentInChildren<InputField>();
    }
}
