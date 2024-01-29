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
        if (text.text == "Monster�������� \n�̵��Ͻðڽ��ϱ�?")
        {
            LoadingSceneManager.LoadScene("MonsterArea");
        }
        else if (text.text == "Boss������ \n�̵��Ͻðڽ��ϱ�?")
        {
            LoadingSceneManager.LoadScene("BossStage");
        }
    }
}
