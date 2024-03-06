
using System.Collections;
using UnityEngine;

public class DragonSound : Sounds
{
    public AudioClip ScreamSound;
    public AudioClip BurningSound;
    public bool attackC = false;
    public override void Attack(int attackType)
    {
        attackC = true;
        audio.spatialBlend = 0;
        audio.clip = AttackSound[attackType];
        audio.Play();
    }
    public override void Attack()
    {
        
    }

    public void Scream()
    {
        audio.maxDistance = 100;
        audio.loop = false;
        audio.spatialBlend = 1;
        audio.clip = ScreamSound;
        audio.Play();
    }

    public void Dead()
    {
        audio.pitch = 0.9f;
        audio.clip = ScreamSound;
        audio.Play();
    }

    public void Burning()
    {
        StartCoroutine(wait());
    }

    public override void Walk()
    {
        if(!attackC)
        {
            audio.spatialBlend = 1;
            audio.clip = WalkSound[0];
            audio.Play();
        }
    }

    public override void StopSound()
    {

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        audio.maxDistance = 30;
        audio.pitch = 1f;
        audio.clip = BurningSound;
        audio.loop = true;
        audio.Play();
    }
}
