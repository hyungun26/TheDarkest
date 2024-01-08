using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class DownButton : MonoBehaviour //IPointerClickHandler
{
    /*public PlayerStat PlayerStat;

    public PlayerStat2 playerUI;
    public TextMeshProUGUI val;
    public TextMeshProUGUI Point;
    public TextMeshProUGUI ability;
    int defaultValue;

    string a;
    int b;
    int c;
    int d;
    //현재 문제 다른곳에 포인트를 투자를 하고 다른곳에 포인트를 빼낼수가있다 그러니까 default값이 건드려진다. 
    //이거에 대한 해결책으로 default값을 저장한 후에 포인트 배분을 해보려고했는데 망할 %가 걸림 
    string q;
    bool persent = false;

    bool asdf = false;

    void Start()
    {
        string qwer = val.text;
        char[] asd = qwer.ToCharArray();
        bool sdf = false;

        for(int i = 0; i < asd.Length; i++)
        {
            if (asd[i] == '%')
            {
                val.text = "";
                asd[i] = default;
                sdf = true;
                for(int j = 0; j < asd.Length-1; j++)
                {
                    val.text += asd[j].ToString(); // % 빠지게만듦
                }
                break;
            }
        }
        
        defaultValue = Int32.Parse(val.text);

        if (sdf)
            val.text += "%";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {            
            a = Point.text;
            c = Int32.Parse(a);
            
            char[] c2 = val.text.ToCharArray();

            for(int i = 0; i < c2.Length; i++)
            {
                if (c2[i] == '%')
                {
                    asdf = true;
                    val.text = "";
                    for(int j = 0; j < c2.Length-1; j++)
                    {
                        val.text += c2[j];
                    }
                }
            }

            int we = Int32.Parse(val.text);

            if (playerUI.savemaxPoint != c && defaultValue < we)
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
                    a = val.text;
                    b = Int32.Parse(a);
                    d = b - 1;
                    val.text = d.ToString();
                }
                else
                {
                    val.text = q.ToString();
                    a = val.text;
                    b = Int32.Parse(a);
                    d = b - 1;
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
                c += 1;
                playerUI.usePoint += 1;
                Point.text = c.ToString();
                return;
            }
            if(asdf)
            {
                val.text += "%";
                asdf = false;
            }
            //UI로 띄우면 좋을듯
        }
    }*/
}
