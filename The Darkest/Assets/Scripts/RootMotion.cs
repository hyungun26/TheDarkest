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
    private void OnAnimatorMove() //��Ʈ ����� ����Ҷ��� �θ�� ���� ���������Ѵ�. �� �Լ��� ����ϸ� ��ũ��Ʈ�� ��� �����ϴ�
    {
        deltaPosition += GetComponent<Animator>().deltaPosition;
        deltaRotation *= GetComponent<Animator>().deltaRotation;
    }
}
