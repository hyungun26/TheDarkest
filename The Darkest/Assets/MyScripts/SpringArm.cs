using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public PlayerController playerController;
    public Animator anim;
    public Transform startPosition;
    public Transform endPosition;
    public Transform mainCamera;
    public LayerMask crashMask;

    public AnimationEvent animEvent;

    public Transform myCam;
    [HideInInspector]
    public float LookupSpeed;
    Vector3 curRot = Vector3.zero;
    public float Offset = 0.5f;
    public Vector2 LookupRange = new Vector2(-60.0f, 80.0f);

    public Vector3 camPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        curRot.x = transform.localRotation.eulerAngles.x;
        curRot.y = transform.parent.localRotation.eulerAngles.y;

        camPos = myCam.localPosition;

        startPosition.localPosition = mainCamera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.MyState == PlayerController.PlayerState.Play)
        {
            curRot.x -= Input.GetAxisRaw("Mouse Y") * LookupSpeed;
            curRot.x = Mathf.Clamp(curRot.x, LookupRange.x, LookupRange.y);

            curRot.y += Input.GetAxisRaw("Mouse X") * LookupSpeed;

            transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
            transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);

            //카메라 Object뚫기 방지
            if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, -camPos.z + Offset, crashMask))
            {
                myCam.localPosition = new Vector3(0, 0, -hit.distance + Offset);
            }
            else
            {
                //if (Mathf.Approximately(startPosition.position.x, myCam.position.x))
                //{
                //    if (!anim.GetBool("Aiming"))
                //    {
                //        myCam.localPosition = camPos;
                //    }
                //}
            }

            if (anim.GetBool("Aiming") && animEvent.ReadyToAim)
            {
                myCam.localPosition = Vector3.Lerp(myCam.localPosition, endPosition.localPosition, 5.0f * Time.deltaTime);
            }
            else if (!anim.GetBool("Aiming"))
            {
                myCam.localPosition = Vector3.Lerp(myCam.localPosition, startPosition.localPosition, 5.0f * Time.deltaTime);
            }
            //매우 안좋은 코드 나중에 바꾸자
        }
    }
}
