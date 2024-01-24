using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    public new Camera camera;
    public Slider slid;
    public Transform child;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slid.transform.position = camera.WorldToScreenPoint(child.position);
    }
}
