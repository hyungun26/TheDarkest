
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSound : Sounds
{
    public AudioClip HealAuraSound;
    public void BowRealese()
    {
        audio.clip = AttackSound[0];
        audio.Play();
    }
    public override void Attack()
    {
        audio.clip = AttackSound[1];
        audio.Play();
    }
    public override void Walk()
    {
        str = SceneManager.GetActiveScene().name;
        
        switch (str)
        {
            case "BossStage":
                audio.clip = WalkSound[1];
                break;
            default:
                audio.clip = WalkSound[0];
                break;
        }
        if (audio.clip.name == HealAuraSound.name)
        {
            return;
        }
        audio.Play();
    }
    public void Heal()
    {
        audio.clip = HealAuraSound;
        audio.Play();
    }

    public void HealStop()
    {
        audio.Stop();
    }

    public override void StopSound()
    {
        if (audio.clip == null)
            return;
        if(audio.clip.name == "활 당기기")
        {
            audio.clip = null;
        }
    }

    public override void Attack(int attackType)
    {
        
    }

    public override void Attacked()
    {
        audio.clip = AttackedSound[0];
        audio.Play();
    }
}
