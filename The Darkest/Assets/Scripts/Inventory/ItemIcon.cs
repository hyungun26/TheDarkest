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
        //����
    }   

    public void OnPointerExit(PointerEventData eventData)
    {
        //����
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        All = GameObject.Find("All").transform;
        Canvas = GameObject.Find("Canvas").transform;
        //�������� 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //������ 
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //������ ������ �����Ѵ�
        if ((Time.time - clickTime) < 0.3f) // ���� Ŭ��
        {
            clickTime = -1;
            Debug.Log("����Ŭ��");
            EquipmentSlot equip = transform.GetComponentInParent<EquipmentSlot>();
            ItemSlot Slot = transform.GetComponentInParent<ItemSlot>();
            if (equip != null)
            {
                //���� inventory�ȿ� ��ĭ�� �ִ� ������ �̵��ؾ��� �� for������ �˻��ؼ� �� slotã�� �ֱ�
                Debug.Log("inventory�� �̵�");
            }
            if (Slot != null)
            {
                //���� ���â�� �ٸ� ��� �ִٸ� �ٲٱ�
                Debug.Log("���â���� �̵�");
            }
            //���߿� ����
        }
        else
            clickTime = Time.time;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //slot = null;
        GetComponent<Image>().raycastTarget = false; // ������ �׸��� ������ �ޱ����� falseó���Ѱ�
        previousParent = transform.parent;
        transform.SetParent(All);
        transform.SetAsLastSibling();
        //�巡�� ����
    }

    public void OnDrag(PointerEventData eventData) //�巡�� ��
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
            //�������� player�ٴڿ� ���� �������� �����ؾ߰���? ������ �Ǵµ� ��ġ�� �� ��������
            PlayerPos = GameObject.FindWithTag("Player");
            Instantiate(IT, PlayerPos.transform);
            IT.transform.localPosition = Vector3.zero;            
            Destroy(this.gameObject);
        }
    }

    private bool IsOverUI() => EventSystem.current.IsPointerOverGameObject();
}
