using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSound : Sounds
{
    public AudioSource Idle;
    public AudioClip IdleSound;
    public AudioClip[] DeadSound;

    private void Update()
    {
        if(Idle.volume != audio.volume)
        {
            Debug.Log("이러면 많이안하겠쭁");
            Idle.volume = audio.volume;
        }
    }

    public override void Attack(int attackType)
    {
        audio.clip = AttackSound[0];
        audio.Play();
    }

    public void Dead()
    {
        audio.clip = DeadSound[0];
        audio.Play();
    }
    public void Hit()
    {
        Idle.clip = DeadSound[1];
        Idle.Play();
    }

    public void StartIdle()
    {
        Idle.clip = IdleSound;
        Idle.Play();
    }

    public override void Attack()
    {
        
    }

    public override void StopSound()
    {
        Idle.clip = null;
        audio.clip = null;
    }

    public override void Walk()
    {
        if(audio.clip != DeadSound[0])
        {
            audio.clip = WalkSound[0];
            audio.Play();
        }
    }

    public override void Attacked()
    {
        
    }
}
