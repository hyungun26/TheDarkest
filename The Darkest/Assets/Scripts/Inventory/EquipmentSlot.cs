using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    public bool have = false;
    ItemIcon i;
    public enum Slot
    {
        Body, Bow, Head, Shoes, Comsumable
    }
    public Slot slot = default;

    void Update()
    {
        if(transform.childCount != 0)
            have = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        
        ItemIcon haveItem = transform.GetComponentInChildren<ItemIcon>();

        ItemIcon item = eventData.pointerDrag.GetComponent<ItemIcon>();
        Item dropItem = item.IT.GetComponent<Item>();
        
        string s = dropItem.equipMent.ToString();
        string sa = slot.ToString();
        if(s == sa)
        {
            if (haveItem != null && dropItem != null)
            {
                //되긴함 문제는 scale이 변경 됨
                haveItem.transform.SetParent(item.previousParent, false);
                haveItem.transform.localScale = Vector3.one;
            }
            eventData.pointerDrag.transform.SetParent(transform);//icon이 slot에서 drop되었을때 현재 ItemSlot class를 가지고 있는
            eventData.pointerDrag.transform.localPosition = Vector2.zero;
        }
    }
}
