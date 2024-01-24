using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSearch : MonoBehaviour
{
    Monster mon;
    private void Start()
    {
        mon = transform.parent.GetComponent<Monster>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mon.PlayerTransform = other.transform;
            mon.Search();
        }
    }
}
