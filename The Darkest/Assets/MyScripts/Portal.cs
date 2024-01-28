using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public UIControll UIControll;
    public RectTransform PortalUI;
    private void OnTriggerEnter(Collider other)
    {
        //들어오면 ui켜지고 player마우스 움직임 통제
        //2개의 선택지가 주어짐 누르는 곳으로 이동 시키기 여기에 Scene을 연동하면 될듯합니다.
        if(other.CompareTag("Player"))
        {
            //ui창이 켜지고 x버튼을 누르면 다시 움직이기
            if (!PortalUI.gameObject.activeSelf)
            {
                PortalUI.gameObject.SetActive(true);
                UIControll.list.Add(PortalUI.gameObject);
            }
            Debug.Log("player가 이동준비중임");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //나가면 ui제거
        if (other.CompareTag("Player"))
        {
            if(PortalUI.gameObject.activeSelf)
            {
                UIControll.list.Remove(PortalUI.gameObject);
                PortalUI.gameObject.SetActive(false);
            }
            Debug.Log("player가 범위를 이탈함");
        }
    }
}
