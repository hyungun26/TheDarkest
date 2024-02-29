using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] ArrowSound;
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
}
