using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneSelectButton : JustButton
{
    public int SceneNumber;
    public TextMeshProUGUI text;
    public override void OnClick()
    {
        if(!UI.gameObject.activeSelf)
        {
            UIControll.list.Add(UI.gameObject);
            UI.gameObject.SetActive(true);
            UI.parent.SetAsLastSibling();
        }
        if (SceneNumber == 1)
            text.text = "Monster�������� \n�̵��Ͻðڽ��ϱ�?";
        else if (SceneNumber == 2)
            text.text = "Boss������ \n�̵��Ͻðڽ��ϱ�?";
    }

}
