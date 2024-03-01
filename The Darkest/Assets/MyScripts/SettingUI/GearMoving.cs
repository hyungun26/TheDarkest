using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GearMoving : MonoBehaviour
{
    protected Slider slid;
    public Sprite changeImage;
    protected Image img;
    protected Sprite prevImg;
    protected GameObject Gear;
    // Start is called before the first frame update
    void Start()
    {
        Gear = this.transform.GetChild(2).gameObject;
        Gear = Gear.transform.GetChild(0).gameObject;
        slid = this.transform.GetComponent<Slider>();
        img = Gear.transform.GetComponent<Image>();
        prevImg = img.sprite;        
    }

    public abstract void Gearmove();

    public void GearM()
    {
        Gear.transform.rotation = Quaternion.Euler(Vector3.forward * -slid.value);
        if (Gear.transform.rotation == Quaternion.Euler(Vector3.forward * 360.0f))
        {
            img.sprite = changeImage;
        }
        else
        {
            img.sprite = prevImg;
        }
    }
}
