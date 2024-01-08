using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    Animator animator;
    public Transform playerSpineTr;

    public Transform mainCameraTr;

    static float test = 90.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator)
            playerSpineTr = animator.GetBoneTransform(HumanBodyBones.Spine);
    }

    public void LateUpdate()
    {
        if(animator.GetBool("Aiming"))
        Operation_boneRotation();
    }

    Vector3 ChestOffset = new Vector3(0.7f, test, 8.0f);
    Vector3 ChestDir = new Vector3();

    void Operation_boneRotation()
    {
        ChestDir = mainCameraTr.position + mainCameraTr.forward;
        playerSpineTr.LookAt(ChestDir);
        playerSpineTr.rotation = mainCameraTr.rotation * Quaternion.Euler(ChestOffset);
    }
}
