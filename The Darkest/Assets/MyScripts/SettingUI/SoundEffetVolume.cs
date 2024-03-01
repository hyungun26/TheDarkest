using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffetVolume : GearMoving
{
    public AudioSource[] effect;
    public override void Gearmove()
    {
        foreach(var i in effect)
        {
            i.volume = slid.value * 0.0027777777777778f;
        }
        GearM();
    }
}
