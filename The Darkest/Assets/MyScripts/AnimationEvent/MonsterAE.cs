using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAE : AnimationEvent
{
    public MonsterSound monsterSound;
    public override void RunSound()
    {
        
    }

    public override void WalkSound()
    {
        monsterSound.Walk();
    }
    public void AttackSound()
    {
        monsterSound.Attack(1);
    }
    public void Attacked()
    {
        monsterSound.Hit();
    }
    public void DeadSound()
    {
        monsterSound.Dead();
    }
    public void Idle()
    {
        monsterSound.StartIdle();
    }
}
