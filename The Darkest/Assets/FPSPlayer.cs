using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    public Transform mySpine;
    public Transform mySpring;
    public Animator myAnim = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        //myAnim.SetFloat("x", x);
        //myAnim.SetFloat("y", z);

        //mySpine.rotation = mySpring.rotation;
    }
}
