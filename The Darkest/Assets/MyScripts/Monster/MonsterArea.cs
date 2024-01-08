using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterArea : MonoBehaviour
{
    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            //Debug.Log("어허 나가면 안돼");
            //이곳에서 몬스터가 영역을 나갔을때 돌아오도록 유도해야함
        }
    }
}
