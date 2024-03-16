using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public UIControll UIControll;
    public RectTransform PortalUI;
    public RectTransform InteractUI;
    public Slider slider;
    private PoolManager pool;
    private bool Teleport = false;
    private float TeleportTime;
    private void OnTriggerEnter(Collider other)
    {        
        //pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        if(UIControll == null)
        {
            UIControll = GameObject.Find("InitCanvas").GetComponent<UIControll>();
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
        //������ ui������ player���콺 ������ ����
        //2���� �������� �־��� ������ ������ �̵� ��Ű�� ���⿡ Scene�� �����ϸ� �ɵ��մϴ�.
        if (other.CompareTag("Player"))
        {
            //uiâ�� ������ x��ư�� ������ �ٽ� �����̱�
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
                slider.gameObject.SetActive(true);
                Teleport = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Teleport && SceneManager.GetActiveScene().name != "The Darkest RestPlace")
        {
            TeleportTime += Time.deltaTime;
            slider.value = TeleportTime;
            if (TeleportTime > 5.0f)
            {
                TeleportTime = 0.0f;
                Teleport = false;
                LoadingSceneManager.LoadScene("The Darkest RestPlace");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //������ ui����
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
                slider.value = TeleportTime;
                slider.gameObject.SetActive(false);
                Teleport = false;
            }
        }
    }
}
