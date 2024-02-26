using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSensitivity : GearMoving
{
    public SpringArm mouseSensitivity;

    public override void Gearmove()
    {
        mouseSensitivity.LookupSpeed = slid.value * 0.1f;
    }
}
