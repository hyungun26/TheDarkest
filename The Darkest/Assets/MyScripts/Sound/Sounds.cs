using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sounds : MonoBehaviour
{
    AudioSource interact;
    protected new AudioSource audio;
    public AudioClip[] WalkSound;
    public AudioClip[] AttackSound;
    public AudioClip[] AttackedSound;
    protected static string str;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        GameObject obj = GameObject.Find("InteractSound");
        interact = obj.GetComponent<AudioSource>();
        audio.volume = interact.volume;
    }

    private void FixedUpdate()
    {
        if(audio.volume != interact.volume)
        {
            audio.volume = interact.volume;
        }
    }

    public abstract void Attacked();
    public abstract void Attack(int attackType);
    public abstract void Attack();
    public abstract void Walk();
    public abstract void StopSound();
}
