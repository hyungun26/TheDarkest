using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNPC : Interact
{
    Collider col;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            col = other.GetComponent<Collider>();
            InteractT.gameObject.SetActive(true);
            InteractT.text = "Press \"Z\"";
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            InteractT.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (InteractT.gameObject.activeSelf)
            {
                col.GetComponent<NPC>().Talk.gameObject.SetActive(true);
                InteractT.gameObject.SetActive(false);
                Debug.Log("zŰ�� �������� ������ ������ player�� �������� ���ѵȴ�.");
            }
        }
    }
}