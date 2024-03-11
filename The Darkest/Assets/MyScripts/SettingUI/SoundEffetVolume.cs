using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffetVolume : GearMoving
{
    public List<AudioSource> list = new List<AudioSource>();
    
    public override void Gearmove()
    {
        foreach(var i in list)
        {
            i.volume = slid.value * 0.0027777777777778f;
        }
        GearM();
    }
}
