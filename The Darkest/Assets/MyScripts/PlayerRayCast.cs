using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public LayerMask mask;

    public TextMeshProUGUI InteractT;

    public float RayDistance = 2.0f;

    public Animator PlayerAnim;
    public Animator CameraAnim;
    public PlayerController PlayerController;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, transform.forward * 2.0f, Color.red);
        if(Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit hit, RayDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Save"))
            {
                InteractT.gameObject.SetActive(true);
                InteractT.text = "Press \"F\"";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PlayerController.Arrow.gameObject.SetActive(false);
                    RayDistance = 0.0f; //텍스트 끄기용
                    InteractT.gameObject.SetActive(false);
                    PlayerAnim.SetTrigger("Save");
                    CameraAnim.enabled = true;
                    CameraAnim.SetTrigger("Save");
                    PlayerController.ChangeState(PlayerController.PlayerState.Heal);
                }
            }
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                InteractT.gameObject.SetActive(false);
            }
        }
        else
        {
            InteractT.gameObject.SetActive(false);
        }
    }
}
