using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitSoundVolume : MonoBehaviour
{
    public GameObject settingUI;
    public SoundVolume soundVol;
    float vol = 0.2777778f;
    new AudioSource audio;
    // Update is called once per frame
    void Update()
    {
        if (settingUI.activeSelf)
        {
            vol = soundVol.var;
        }
        else
        {
            audio = GameObject.Find("Bgm").GetComponent<AudioSource>();
            audio.volume = vol;
        }
    }
}
