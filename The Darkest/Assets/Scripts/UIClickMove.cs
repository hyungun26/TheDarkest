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

        //���⿡�� list������ ó���� �ϸ� ���� Ŭ���������� ���� ui�� ������ ������ esc�� �������� ������� ������
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
