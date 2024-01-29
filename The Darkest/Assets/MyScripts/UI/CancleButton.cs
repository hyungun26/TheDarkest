using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancleButton : JustButton
{
    public override void OnClick()
    {
        this.transform.parent.gameObject.SetActive(false);
        UIControll.list.Remove(UI.gameObject);
    }
}
