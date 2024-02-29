using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolume : GearMoving
{
    public AudioSource bgm;
    
    public override void Gearmove()
    {
        bgm.volume = slid.value * 0.0027777777777778f; //이러면 유사 1이 나옴
    }
}
