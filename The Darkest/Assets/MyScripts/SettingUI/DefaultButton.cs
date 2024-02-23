using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButton : JustButton
{
    public Slider[] slids;

    public override void OnClick()
    {
        foreach(var i in slids)
        {
            i.value = 100;
        }
    }
}
