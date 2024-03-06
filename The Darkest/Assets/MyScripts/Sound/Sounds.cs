using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sounds : MonoBehaviour
{
    protected new AudioSource audio;
    public AudioClip[] AttackSound;
    public AudioClip[] WalkSound;
    protected static string str;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public abstract void Attack(int attackType);
    public abstract void Attack();
    public abstract void Walk();
    public abstract void StopSound();
}
