using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            player.PlayerHitCode(30f);
        }
    }

}
