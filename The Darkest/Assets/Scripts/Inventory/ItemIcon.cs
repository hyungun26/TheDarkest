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
            Debug.Log("더블클릭");
            EquipmentSlot equip = transform.GetComponentInParent<EquipmentSlot>();
            ItemSlot Slot = transform.GetComponentInParent<ItemSlot>();
            if (equip != null)
            {
                //조건 inventory안에 빈칸이 있는 곳으로 이동해야함 즉 for문으로 검사해서 빈 slot찾고 넣기
                Debug.Log("inventory로 이동");
            }
            if (Slot != null)
            {
                //조건 장비창에 다른 장비가 있다면 바꾸기
                Debug.Log("장비창으로 이동");
            }
            //나중에 구현
        }
        else
            clickTime = Time.time;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //slot = null;
        GetComponent<Image>().raycastTarget = false; // 이전에 그림에 영향을 받기위해 false처리한것
        previousParent = transform.parent;
        transform.SetParent(All);
        transform.SetAsLastSibling();
        //드래그 시작
    }

    public void OnDrag(PointerEventData eventData) //드래그 중
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
 
        if(transform.parent == All)
        {
            transform.SetParent(previousParent);
            transform.localPosition = Vector2.zero;
        }

        if(!IsOverUI())
        {
            //버렸으면 player바닥에 버린 아이템을 생성해야겠죠? 생성은 되는데 위치가 잘 안잡히네
            PlayerPos = GameObject.FindWithTag("Player");
            Instantiate(IT, PlayerPos.transform);
            IT.transform.localPosition = Vector3.zero;            
            Destroy(this.gameObject);
        }
    }

    private bool IsOverUI() => EventSystem.current.IsPointerOverGameObject();
}
