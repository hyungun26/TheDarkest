using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    Vector3 deltaPosition = Vector3.zero;
    Quaternion deltaRotation = Quaternion.identity;
    
    private void FixedUpdate()
    {
        transform.parent.Translate(deltaPosition, Space.World);
        deltaPosition = Vector3.zero;
        transform.parent.rotation *= deltaRotation;
        deltaRotation = Quaternion.identity;
    }
    private void OnAnimatorMove() //루트 모션을 사용할때는 부모와 같이 움직여야한다. 이 함수를 사용하면 스크립트로 제어가 가능하다
    {
        deltaPosition += GetComponent<Animator>().deltaPosition;
        deltaRotation *= GetComponent<Animator>().deltaRotation;
    }
}
