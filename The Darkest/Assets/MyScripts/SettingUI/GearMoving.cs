using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GearMoving : MonoBehaviour
{
    public Slider slid;
    public Sprite changeImage;
    
    [HideInInspector]
    public Image img;
    [HideInInspector]
    public Sprite prevImg;
    // Start is called before the first frame update
    void Start()
    {
        slid.value = 100;
        img = this.transform.GetComponent<Image>();
        prevImg = img.sprite;        
    }

    private void FixedUpdate()
    {
        Gearmove();
        this.transform.rotation = Quaternion.Euler(Vector3.forward * -slid.value);
        if (this.transform.rotation == Quaternion.Euler(Vector3.forward * 360.0f))
        {
            img.sprite = changeImage;
        }
        else
        {
            img.sprite = prevImg;
        }
    }

    public abstract void Gearmove();
}
