using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DownButton2 : MonoBehaviour, IPointerClickHandler
{
    public PlayerStat Once;
    public UpButton2 upbuttonNum;
    public TextMeshProUGUI LvPoint;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {         
            if(upbuttonNum.num == 0) return;
            upbuttonNum.num -= 1;
            int num = int.Parse(LvPoint.text) + 1;
            LvPoint.text = num.ToString();
            Once.once = true; 
        }
    }
}
