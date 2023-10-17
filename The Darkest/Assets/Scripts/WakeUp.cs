using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUp : Dragon
{
    
    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    Debug.Log("확인");
        //    range = true;
        //}
    }
}
