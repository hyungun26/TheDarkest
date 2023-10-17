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
                //�Ǳ��� ������ scale�� ���� ��
                haveItem.transform.SetParent(item.previousParent, false);
                haveItem.transform.localScale = Vector3.one;
            }
            eventData.pointerDrag.transform.SetParent(transform);//icon�� slot���� drop�Ǿ����� ���� ItemSlot class�� ������ �ִ�
            eventData.pointerDrag.transform.localPosition = Vector2.zero;
        }
    }
}
