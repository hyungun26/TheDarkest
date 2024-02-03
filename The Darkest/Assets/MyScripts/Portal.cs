using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public UIControll UIControll;
    public RectTransform PortalUI;
    public RectTransform InteractUI;
    private bool Teleport = false;
    private float TeleportTime;
    private void OnTriggerEnter(Collider other)
    {
        if(UIControll == null)
        {
            UIControll = GameObject.Find("Canvas").GetComponent<UIControll>();
        }
        if (PortalUI == null)
        {
            PortalUI = GameObject.Find("PortalUI").GetComponent<RectTransform>();
            PortalUI = PortalUI.GetChild(0).GetComponent<RectTransform>();
        }
        if (InteractUI == null)
        {
            InteractUI = GameObject.Find("InteractUI").GetComponent<RectTransform>();
            InteractUI = InteractUI.GetChild(0).GetComponent<RectTransform>();
        }
        //들어오면 ui켜지고 player마우스 움직임 통제
        //2개의 선택지가 주어짐 누르는 곳으로 이동 시키기 여기에 Scene을 연동하면 될듯합니다.
        if (other.CompareTag("Player"))
        {
            //ui창이 켜지고 x버튼을 누르면 다시 움직이기
            if(SceneManager.GetActiveScene().name == "The Darkest RestPlace")
            {
                if (!PortalUI.gameObject.activeSelf)
                {
                    PortalUI.gameObject.SetActive(true);
                    PortalUI.parent.SetAsLastSibling();
                    UIControll.list.Add(PortalUI.gameObject);
                }
            }
            else
            {
                Teleport = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Teleport && SceneManager.GetActiveScene().name != "The Darkest RestPlace")
        {
            TeleportTime += Time.deltaTime;
            if(TeleportTime > 5.0f)
            {
                TeleportTime = 0.0f;
                Teleport = false;
                LoadingSceneManager.LoadScene("The Darkest RestPlace");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //나가면 ui제거
        if (other.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "The Darkest RestPlace")
            {
                if (PortalUI.gameObject.activeSelf)
                {
                    UIControll.list.Remove(PortalUI.gameObject);
                    PortalUI.gameObject.SetActive(false);
                    if (InteractUI.gameObject.activeSelf)
                    {
                        InteractUI.gameObject.SetActive(false);
                        UIControll.list.Remove(InteractUI.gameObject);
                    }
                }
            }
            else
            {
                TeleportTime = 0.0f;
                Teleport = false;
            }
        }
    }
}
