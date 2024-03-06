using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAE : AnimationEvent
{
    public PlayerController playerController;
    public PlayerSound playerSound;
    public PoolManager pool;

    public GameObject Arrow;
    public GameObject ArrowClone;
    public Transform Arch2;
    public Transform RightHand;

    public bool animAming = false;
    public bool ReadyToShoot = false;
    public bool ReadyToAim = true;
    public bool SAim = false;
    public bool PlayerDown = false;
    public bool RestEnd = false;

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
        playerSound.BowRealese();
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
        playerSound.StopSound();
        ReadyToAim = false;
    }

    public override void WalkSound()
    {
        if (!playerController.Run)
        {
            if (playerController.Walk)
            {
                playerSound.Walk();
            }
        }
    }
    public override void RunSound()
    {
        if (playerController.Run)
        {
            playerSound.Walk();
        }
        else
            playerSound.StopSound();
    }
    public void WakeUp()
    {
        PlayerDown = true;
    }

    public void OnRayRange()
    {
        PlayerRayCast.RayDistance = 2.0f;
        RestEnd = true;
    }
}
