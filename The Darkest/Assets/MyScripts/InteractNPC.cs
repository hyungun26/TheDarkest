using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNPC : Interact
{
    Collider col;
    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            col = other.GetComponent<Collider>();
            InteractT.gameObject.SetActive(true);
            InteractT.text = "Press \"Z\"";
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            InteractT.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(InteractT.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                col.GetComponent<NPC>().Store.gameObject.SetActive(true);
                Debug.Log("여기에서 보내줘야겠음");
            }
        }
    }
}
