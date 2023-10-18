using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UpButton2 : MonoBehaviour, IPointerClickHandler
{
    public PlayerStat Once;
    public int num = 0;
    public TextMeshProUGUI LvPoint;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {         
            
            int pointUse = int.Parse(LvPoint.text.ToString());
            if(pointUse == 0)
            {
                 Debug.Log("포인트없음"); 
                 return;
            }
            pointUse -= 1;
            LvPoint.text = pointUse.ToString();
            num++;
            Once.once = true; 
            Debug.Log("눌림");
        }
    }
}
