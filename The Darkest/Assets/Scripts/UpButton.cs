using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpButton : MonoBehaviour, IPointerClickHandler
{
    public PlayerStat PlayerStat;

    public PlayerStat2 playerUI;
    public TextMeshProUGUI val;
    public TextMeshProUGUI Point;
    public TextMeshProUGUI ability;
    
    string a;
    int b;
    int c;
    int d;

    string q;
    bool persent = false;

    int maxAbility = 0;

    int currentval = 0;

    bool asdf = false;
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
            a = Point.text;
            c = Int32.Parse(a);

            char[] c3 = val.text.ToCharArray();

            for (int i = 0; i < c3.Length; i++)
            {
                if (c3[i] == '%')
                {
                    asdf = true;
                    persent = true;
                    val.text = "";
                    for (int j = 0; j < c3.Length - 1; j++)
                    {
                        val.text += c3[j];
                    }
                }
            }

            currentval = Int32.Parse(val.text);

            char[] c2 = val.text.ToCharArray();
            if (c > 0 && maxAbility > currentval)
            {
                for (int i = 0; i < c2.Length; i++) //검사 결과 %가 있으면
                {
                    if (c2[i] == '%')
                    {
                        persent = true;
                        break;
                    }
                    q += c2[i].ToString();
                }

                if(!persent)
                {
                    a = val.text; //능력치 값
                    b = Int32.Parse(a); //int형으로 변환
                    d = b + 1; // 예를들어 101이 들어감
                    val.text = d.ToString();
                }
                else
                {
                    val.text = q.ToString();
                    a = val.text;
                    b = Int32.Parse(a);
                    d = b + 1;
                    val.text = d.ToString() + "%";
                    q = default;
                }
                switch (ability.text)
                {
                    case "Damage":
                        PlayerStat.Damage = d;
                        break;
                    case "Health":
                        PlayerStat.Health = d;
                        break;
                    case "Stamina":
                        PlayerStat.Stamina = d;
                        break;
                    case "Defence":
                        PlayerStat.Defence = d;
                        break;
                    case "Critical":
                        PlayerStat.Critical = d;
                        break;
                }

                c -= 1;
                playerUI.usePoint -= 1;
                Point.text = c.ToString();
                return;
            }
            if (asdf)
            {
                val.text += "%";
                asdf = false;
            }
            //포인트 부족하면 오는곳 UI로 띄우면 좋을듯
        }
    }
}
