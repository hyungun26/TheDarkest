using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroButtons : JustButton
{
    
    RectTransform All;
    UIControll uIControll;
    void Start()
    {
        All = GameObject.Find("All").GetComponent<RectTransform>();
        uIControll = GameObject.Find("InitCanvas").GetComponent<UIControll>();
    }
    public override void OnClick()
    {
        //시작 scene으로 넘어가기전에 player canvas playerstatus등등 필요한 object를 담아둔 Scene을 만들어야함
        All.GetChild(1).gameObject.SetActive(true);
        All.GetChild(2).gameObject.SetActive(true);
        SceneManager.LoadScene("The Darkest RestPlace");
    }

    public void SettingClick()
    {
        uIControll.SettingControll();
    }

    public void ExitButton()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
