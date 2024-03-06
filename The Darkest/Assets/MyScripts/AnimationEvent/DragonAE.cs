using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAE : AnimationEvent
{
    public DragonSound dragonSound;
    public Transform DragonHead;
    public GameObject fire;
    public GameObject fireBreath;
    bool fireTF = false;

    //Monster Animator Controll
    public bool Scream = false;
    public bool Fight = true;

    //Dragon Anim State
    public void OnChaing()
    {
        Scream = true;
    }

    public void OnAttack()
    {
        Fight = true;
    }

    public void OnUnAttack()
    {
        Fight = false;
    }

    public void OnFireActive()
    {
        fireTF = !fireTF;
        fire.SetActive(fireTF);
        fire.transform.localScale = Vector3.one * 40f;
    }
    public void OnFireGrow()
    {
        fire.transform.localScale *= 1.2f;
    }
    public void OnFireCreatActive()
    {
        fireTF = !fireTF;
        fireBreath.SetActive(fireTF);
    }

    public override void WalkSound()
    {
        dragonSound.Walk();
    }

    public override void RunSound()
    {
        dragonSound.Walk();
    }

    public void Screaming()
    {
        dragonSound.Scream();
    }
    public void Dead()
    {
        dragonSound.Dead();
    }

    public void Burning()
    {
        dragonSound.Burning();
    }
    public void attackCheck()
    {
        dragonSound.attackC = false;
    }
}
