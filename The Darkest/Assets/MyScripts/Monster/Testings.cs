using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testings : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MeshRenderer>().material.color = Color.black;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
