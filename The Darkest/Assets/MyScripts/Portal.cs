using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public UIControll UIControll;
    public RectTransform PortalUI;
    public RectTransform InteractUI;
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
                PortalUI.parent.SetAsLastSibling();
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
                if (InteractUI.gameObject.activeSelf)
                {
                    InteractUI.gameObject.SetActive(false);
                    UIControll.list.Remove(InteractUI.gameObject);
                }
            }
            Debug.Log("player�� ������ ��Ż��");
        }
    }
}
