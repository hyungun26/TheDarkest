using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSearching : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.CompareTag("Player"))
        {
            //여기에서 공격을 하라고 상태를 바꿔주면 됨
        }
    }
}
