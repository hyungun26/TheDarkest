using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancleButton : JustButton
{
    public UIControll UIControll;
    public RectTransform PortalUI;
    public override void OnClick()
    {
        this.transform.parent.gameObject.SetActive(false);
        UIControll.list.Remove(PortalUI.gameObject);
    }
}
