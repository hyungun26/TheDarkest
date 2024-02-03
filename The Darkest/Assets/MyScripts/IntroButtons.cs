using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IntroButtons : JustButton
{
    public override void OnClick()
    {
        //시작 scene으로 넘어가기전에 player canvas playerstatus등등 필요한 object를 담아둔 Scene을 만들어야함
        SceneManager.LoadScene("The Darkest RestPlace");
    }

    public void ExitButton()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
