using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YesButton : JustButton
{
    public TextMeshProUGUI text;
    public RectTransform SelectUI;
    public override void OnClick()
    {
        this.transform.parent.gameObject.SetActive(false);
        UIControll.list.Remove(UI.gameObject);
        if(SelectUI.gameObject.activeSelf)
        {
            SelectUI.gameObject.SetActive(false);
            UIControll.list.Remove(SelectUI.gameObject);
        }
        if (text.text == "Monster영역으로 \n이동하시겠습니까?")
        {
            LoadingSceneManager.LoadScene("MonsterArea");
        }
        else if (text.text == "Boss룸으로 \n이동하시겠습니까?")
        {
            LoadingSceneManager.LoadScene("BossStage");
        }
    }
}
