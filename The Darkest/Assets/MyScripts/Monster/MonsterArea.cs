using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterArea : MonoBehaviour
{
    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            Debug.Log("어허 나가면 안돼");
        }
    }
}
