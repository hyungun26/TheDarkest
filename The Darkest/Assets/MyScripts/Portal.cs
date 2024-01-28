using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public UIControll UIControll;
    public RectTransform PortalUI;
    private void OnTriggerEnter(Collider other)
    {
        //������ ui������ player���콺 ������ ����
        //2���� �������� �־��� ������ ������ �̵� ��Ű�� ���⿡ Scene�� �����ϸ� �ɵ��մϴ�.
        if(other.CompareTag("Player"))
        {
            //uiâ�� ������ x��ư�� ������ �ٽ� �����̱�
            if (!PortalUI.gameObject.activeSelf)
            {
                PortalUI.gameObject.SetActive(true);
                UIControll.list.Add(PortalUI.gameObject);
            }
            Debug.Log("player�� �̵��غ�����");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //������ ui����
        if (other.CompareTag("Player"))
        {
            if(PortalUI.gameObject.activeSelf)
            {
                UIControll.list.Remove(PortalUI.gameObject);
                PortalUI.gameObject.SetActive(false);
            }
            Debug.Log("player�� ������ ��Ż��");
        }
    }
}
