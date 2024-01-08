using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler
{     
    public void OnDrop(PointerEventData eventData)
    {
        ItemIcon icon = transform.GetComponentInChildren<ItemIcon>(); //가만히 있던 icon
        ItemIcon icon2 = eventData.pointerDrag.GetComponent<ItemIcon>(); //드래그 중인 icon

        eventData.pointerDrag.transform.SetParent(transform, false);//icon이 slot에서 drop되었을때 현재 ItemSlot class를 가지고 있는
        eventData.pointerDrag.transform.localScale = Vector3.one;
        eventData.pointerDrag.transform.localPosition = Vector2.zero;
        //slot의 자식으로 이동

        if (icon != null && icon2 != null) // 위치 바꾸기
        {
            Item iconitem = icon.IT.GetComponent<Item>();
            Item iconitem2 = icon2.IT.GetComponent<Item>();
            if (icon2.previousParent.name == "Frame") // 착용 중인장비면
            {
                if (iconitem.equipMent == iconitem2.equipMent)
                {
                    //정상 교환
                    icon.transform.SetParent(icon2.previousParent, false);
                }
                else
                {
                    //착용 부위가 아님
                    icon2.transform.SetParent(icon2.previousParent, false);
                }
            }
            else
            {
                icon.transform.SetParent(icon2.previousParent, false);
            }
        }
    }
}
