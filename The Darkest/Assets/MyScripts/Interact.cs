using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    public TextMeshProUGUI InteractT;
    public abstract void OnTriggerEnter(Collider other);

    public abstract void OnTriggerExit(Collider other);
}
