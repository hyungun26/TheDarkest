using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingSound : MonoBehaviour
{
    new public AudioSource audio;
    public AudioClip[] WalkSound;
    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Ground"))
        //{
        //    Debug.Log("�߼Ҹ�");
        //    audio.clip = WalkSound[0];
        //    audio.Play();
        //}
    }
}
