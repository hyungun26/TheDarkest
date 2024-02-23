using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearMoving : MonoBehaviour
{
    public Slider slid;
    public Sprite changeImage;
    public SpringArm mouseSensitivity;
    Image img;
    Sprite prevImg;
    // Start is called before the first frame update
    void Start()
    {
        slid.value = 60;
        img = this.transform.GetComponent<Image>();
        prevImg = img.sprite;        
    }

    void FixedUpdate()
    {
        mouseSensitivity.LookupSpeed = slid.value * 0.1f;
        this.transform.rotation = Quaternion.Euler(Vector3.forward * -slid.value);
        if(this.transform.rotation == Quaternion.Euler(Vector3.forward * 360.0f))
        {
            img.sprite = changeImage;
        }
        else
        {
            img.sprite = prevImg;
        }
    }
}
