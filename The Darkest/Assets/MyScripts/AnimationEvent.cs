using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public PoolManager pool;
    //Player Animator Controll    
    public GameObject Arrow;
    public GameObject ArrowClone;
    public Transform Arch2;
    public Transform RightHand;

    public Transform DragonHead;
    public GameObject fire;
    public GameObject fireBreath;
    bool fireTF = false;

    public bool animAming = false;
    public bool ReadyToShoot = false;
    public bool ReadyToAim = true;
    public bool SAim = false;
    public bool PlayerDown = false;
    public bool RestEnd = false;

    //Monster Animator Controll
    public bool Scream = false;
    public bool Fight = true;

    public PlayerRayCast PlayerRayCast;

    

    public void OnActiveArrow()
    {
        Arrow.SetActive(true);
    }

    public void OnInactiveArrow()
    {
        Arrow.SetActive(false);
    }

    public void OnCreateArrow()
    {
        pool.Get(1);
        //Instantiate(ArrowClone, Arrow.transform.position, Arrow.transform.rotation);
    }

    public void Aiming()
    {
        SAim = true;
    }

    public void ResetAiming()
    {
        SAim = false;
    }

    public void OnPullBow()
    {
        animAming = true;
    }

    public void OnReleaseBow()
    {
        animAming = false;
        ReadyToShoot = false;
    }

    public void OnPossibleShoot()
    {
        ReadyToShoot = true;
    }

    public void OnPossibleAim()
    {
        ReadyToAim = true;
    }

    public void OnUnPossibleAim()
    {
        ReadyToAim = false;
    }

    public void WakeUp()
    {
        PlayerDown = true;
    }

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

    public void OnRayRange()
    {
        PlayerRayCast.RayDistance = 2.0f;
        RestEnd = true;
    }
}
