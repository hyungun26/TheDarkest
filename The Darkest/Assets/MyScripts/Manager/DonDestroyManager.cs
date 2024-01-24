using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestroyManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
