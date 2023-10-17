using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyIK : MonoBehaviour
{
    public Transform RightHand;    
    public Transform Arch2;
    public Transform Arch2Oripos;
    public float IKWeight;
    Animator myAnim = null;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        Arch2Oripos.localPosition = Arch2.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Toggling());
        }
    }

    IEnumerator Toggling()
    {
        float value = 1.0f;
        float dir = Mathf.Approximately(IKWeight, 0.0f)? 1.0f : -1.0f; 
        while(value > 0.0f)
        {
            float delta = Time.deltaTime;
            value -= delta;
            IKWeight += delta * dir;
            if(IKWeight > 0.9f)
            {
                Arch2.localPosition = RightHand.localPosition;
            }
            yield return null;
        }
        if(IKWeight < 0.9f)
        {
            Arch2.localPosition = Arch2Oripos.localPosition;
        }
        IKWeight = dir > 0.0f ? 1.0f : 0.0f;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        myAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, IKWeight);
        myAnim.SetIKPosition(AvatarIKGoal.RightHand, Arch2.position); //SetIkPosition함수는 OnAnimatorIK함수에서만 동작한다.
    }
}
