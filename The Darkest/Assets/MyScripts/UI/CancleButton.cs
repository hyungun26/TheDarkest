using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancleButton : JustButton
{
    public override void OnClick()
    {
        UIControll.list.Remove(UI.gameObject);
        UIControll.playerController.enabled = true;
        this.transform.parent.gameObject.SetActive(false);
    }
}
