using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DownButton2 : MonoBehaviour, IPointerClickHandler
{
    public PlayerStat Once;
    public UpButton2 upbuttonNum;
    public TextMeshProUGUI LvPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {         
            if(upbuttonNum.num == 0)
            {
                Debug.Log("포인트 없음");
                return;
            }
            upbuttonNum.num -= 1;
            int num = int.Parse(LvPoint.text) + 1;
            LvPoint.text = num.ToString();
            Once.once = true; 
            Debug.Log("눌림");
        }
    }
}
