using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
    IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform All;
    Transform Canvas;
    public Transform previousParent;
    public GameObject IT;
    GameObject PlayerPos;


    float clickTime = 0;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //진입
    }   

    public void OnPointerExit(PointerEventData eventData)
    {
        //나감
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        All = GameObject.Find("All").transform;
        Canvas = GameObject.Find("Canvas").transform;
        previousParent = transform.parent;
        //눌렀을때 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //땠을때
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //누르고 땠을때 반응한다
        if ((Time.time - clickTime) < 0.3f) // 더블 클릭
        {
            
            clickTime = -1;
            EquipmentSlot equip = transform.GetComponentInParent<EquipmentSlot>();
            ItemSlot Slot = transform.GetComponentInParent<ItemSlot>();
            if (equip != null) // 착용중 이라는 뜻
            {
                bool active = false;
                GameObject status = GameObject.Find("All").transform.Find("Inventory").gameObject;
                if(!status.activeSelf && status != null)
                {
                    status.SetActive(true);
                    active = true;
                }
                GameObject obj = GameObject.Find("Content");
                ItemSlot[] itemSlots = obj.GetComponentsInChildren<ItemSlot>();
                for (int i = 0; i < itemSlots.Length; i++)
                {
                    ItemIcon check = itemSlots[i].GetComponentInChildren<ItemIcon>();
                    if (check == null)
                    {
                        this.transform.SetParent(itemSlots[i].transform, false);
                        break;
                    }
                }
                if(active)
                {
                    status.SetActive(false);
                }
            }
            if (Slot != null) //slot에 있다는뜻
            {
                bool active = false;
                GameObject status = GameObject.Find("All").transform.Find("PlayerStatus").gameObject;
                if(!status.activeSelf && status != null)
                {
                    status.SetActive(true);
                    active = true;
                }
                Item Check = IT.GetComponent<Item>();
                string str = Check.equipMent.ToString();
                GameObject s = GameObject.Find(str); // find함수에 문제점 장비창이 비활성화 되어있을때는 찾지못한다
                EquipmentSlot equipmentSlot = s.GetComponentInChildren<EquipmentSlot>();
                if(equipmentSlot.have == true) //착용중이면
                {
                    ItemIcon haveItem = equipmentSlot.GetComponentInChildren<ItemIcon>();
                    haveItem.transform.SetParent(previousParent, false);
                    this.transform.SetParent(equipmentSlot.transform, false);
                }
                else //착용중이 아니면
                {
                    this.transform.SetParent(equipmentSlot.transform, false);
                }
                equipmentSlot.pStat.once = true;
                if(active)
                {
                    EquipmentSlot eq = transform.parent.GetComponent<EquipmentSlot>(); 
                    if(eq != null)
                    {
                        eq.have = true;
                    }
                    status.SetActive(false);
                }
            }
        }
        else
            clickTime = Time.time;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false; // 이전에 그림에 영향을 받기위해 false처리한것
        transform.SetParent(All, false);
        transform.SetAsLastSibling();
        //드래그 시작
    }

    public void OnDrag(PointerEventData eventData) //드래그 중
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) // 드래그 끝
    {
        GetComponent<Image>().raycastTarget = true;
 
        if(transform.parent == All)
        {
            transform.SetParent(previousParent, false);
            transform.localPosition = Vector2.zero;
        }

        if(!IsOverUI()) // 밖으로 나가지면
        {
            PlayerPos = GameObject.FindWithTag("Player");
            Instantiate(IT, PlayerPos.transform);
            IT.transform.localPosition = Vector3.zero;            
            Destroy(this.gameObject);
        }
    }

    private bool IsOverUI() => EventSystem.current.IsPointerOverGameObject();
}
