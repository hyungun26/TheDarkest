using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class UpButton2 : MonoBehaviour, IPointerClickHandler
{
    public DataManager dataManager;
    public PlayerStat Once;
    public int num = 0;
    public TextMeshProUGUI LvPoint;
    public TextMeshProUGUI ability;
    public TextMeshProUGUI abilityPoint;
    int maxAbility = 0;
    void Start()
    {
        switch (ability.text)
        {
            case "Damage":
                maxAbility = 9999;
                break;
            case "Health":
                maxAbility = 9999;
                break;
            case "Stamina":
                maxAbility = 9999;
                break;
            case "Defence":
                maxAbility = 9999;
                break;
            case "Critical":
                maxAbility = 100;
                break;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            string s = abilityPoint.text.ToString();
            s = s.Replace("%", "");
            int ap = int.Parse(s);
            if (maxAbility <= ap) return;
            int pointUse = int.Parse(LvPoint.text.ToString());
            if(pointUse == 0) return;
            pointUse -= 1;
            LvPoint.text = pointUse.ToString();
            num++;
            Once.once = true; 
        }
    }
}
