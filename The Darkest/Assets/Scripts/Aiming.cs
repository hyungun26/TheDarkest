using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Aiming : MonoBehaviour
{
    public AnimationEvent animEvent;
    public Animator anim;
    public RectTransform tr;
    RectTransform oriPos;

    [SerializeField]
    float Charging = 0.85f;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<RectTransform>();
        tr.localScale = new Vector3(2.7f, 2.7f, 2.7f);
        oriPos = GetComponent<RectTransform>();
        oriPos.localScale = new Vector3(2.7f, 2.7f, 2.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.localScale.x > 1.0f && anim.GetBool("Aiming") && animEvent.SAim) //aim canvas 크기 조절
        {
            this.transform.localScale -= new Vector3(1, 1, 1) * Charging * Time.deltaTime;
        }
        else if(!anim.GetBool("Aiming") || !animEvent.SAim)
        {
            this.transform.localScale = new Vector3(2.7f, 2.7f , 2.7f);
        }
    }
}
