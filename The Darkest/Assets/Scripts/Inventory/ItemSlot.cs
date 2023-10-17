using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler
{     
    public void OnDrop(PointerEventData eventData)
    {
        ItemIcon icon = transform.GetComponentInChildren<ItemIcon>(); //�ٲٷ����ߴ� slot�ȿ� icon�� ��
        ItemIcon icon2 = eventData.pointerDrag.GetComponent<ItemIcon>();
        if(icon != null && icon2 != null)
        {
            icon.transform.SetParent(icon2.previousParent, false);
        }
        eventData.pointerDrag.transform.SetParent(transform);//icon�� slot���� drop�Ǿ����� ���� ItemSlot class�� ������ �ִ�
        eventData.pointerDrag.transform.localPosition = Vector2.zero;
        //slot�� �ڽ����� �̵�
    }
}
