using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterArea : MonoBehaviour
{
    Monster mon;
    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            mon = other.GetComponent<Monster>();
            if(mon != null)
            {
                mon.MonsterArea = GameObject.Find("Flag").GetComponent<Transform>();
                Debug.Log(mon.MonsterArea);
                mon.outOfRange = true;
            }            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            mon = other.GetComponent<Monster>();
            if(mon.outOfRange && mon.State == Monster.MonsterState.Idle)
            {
                Debug.Log("들어올수있나");
                mon.outOfRange = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            Monster mon = other.GetComponent<Monster>();
        }
    }
}
