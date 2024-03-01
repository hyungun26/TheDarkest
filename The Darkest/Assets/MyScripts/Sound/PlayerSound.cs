using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    new AudioSource audio;
    public AudioClip[] ArrowSound;
    public AudioClip[] WalkSound;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    public void BowRealese()
    {
        audio.clip = ArrowSound[0];
        audio.Play();
    }
    public void ShotArrow()
    {
        audio.clip = ArrowSound[1];
        audio.Play();
    }
    public void Walk()
    {
        audio.clip = WalkSound[0];
        audio.Play();
    }

    public void StopSound()
    {
        if (audio.clip == null)
            return;
        if(audio.clip.name == "활 당기기")
        {
            audio.clip = null;
        }
    }
}
