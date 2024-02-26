using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolume : GearMoving
{
    AudioSource bgm;
    [HideInInspector]
    public float var = 0.27777777777778f;
    public override void Gearmove()
    {
        bgm = GameObject.Find("Bgm").GetComponent<AudioSource>();
        if (bgm == null)
        {
            return;
        }

        var = slid.value * 0.0027777777777778f; //이러면 유사 1이 나옴
        bgm.volume = var; 
    }
}
