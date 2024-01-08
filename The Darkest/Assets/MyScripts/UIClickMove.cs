using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickMove : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 StartPos;
    private Vector2 BeginPos;
    public UIControll ui;

    public void OnPointerDown(PointerEventData eventData)
    {
        BeginPos = this.transform.parent.position;
        StartPos = eventData.position;

        //여기에서 list에대한 처리를 하면 내가 클릭을했을때 누른 ui가 앞으로 나오고 esc을 눌렀을때 순서대로 꺼진다
        for (int i = 0; i < ui.list.Count; i++)
        {
            if (ui.list[i] == this.transform.parent.gameObject)
                ui.list.Remove(this.transform.parent.gameObject);
        }
        ui.list.Add(this.transform.parent.gameObject);
        this.transform.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.parent.position = BeginPos + (eventData.position - StartPos);
    }
}
