using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler
{     
    public void OnDrop(PointerEventData eventData)
    {
        ItemIcon icon = transform.GetComponentInChildren<ItemIcon>(); //바꾸려고했던 slot안에 icon이 들어감
        ItemIcon icon2 = eventData.pointerDrag.GetComponent<ItemIcon>();
        if(icon != null && icon2 != null)
        {
            icon.transform.SetParent(icon2.previousParent, false);
        }
        eventData.pointerDrag.transform.SetParent(transform);//icon이 slot에서 drop되었을때 현재 ItemSlot class를 가지고 있는
        eventData.pointerDrag.transform.localPosition = Vector2.zero;
        //slot의 자식으로 이동
    }
}
